using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UNCDF.Layers.Model;
using UNCDF.Layers.Business;
using UNCDF.Utilities;
using System.Transactions;
using System.IO;


namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class TimeLineTestimonialTranslateController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;


        public TimeLineTestimonialTranslateController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }
        // POST api/values
        [HttpPost]
        [Route("0/GetTimeLineTestimonialTranslates")]
        public TimeLineTestimonialTranslatesResponse GetTimeLineTestimonialTranslates([FromBody] TimeLineTestimonialTranslateRequest request)
        {
            TimeLineTestimonialTranslatesResponse response = new TimeLineTestimonialTranslatesResponse();
            List<MTimeLineTestimonialTranslate> TimeLineTestimonialTranslateList = new List<MTimeLineTestimonialTranslate>();
            MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate = new MTimeLineTestimonialTranslate();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MTimeLineTestimonialTranslate.TimeLineTestId = request.TimeLineTestimonialTranslate.TimeLineTestId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;
            int Val = 0;

            TimeLineTestimonialTranslateList = BTimeLineTestimonialTranslate.Lis(MTimeLineTestimonialTranslate, ref Val);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.TimeLineTestimonialTranslates = TimeLineTestimonialTranslateList.ToArray();


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLine Testimonial Translates");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLine Testimonial Translates");
            }

            return response;
        }


        [HttpPost]
        [Route("0/GetTimeLineTestimonialTranslate")]
        public TimeLineTestimonialTranslateResponse GetTimeLineTestimonialTranslate([FromBody] TimeLineTestimonialTranslateRequest request)
        {
            TimeLineTestimonialTranslateResponse response = new TimeLineTestimonialTranslateResponse();
            MTimeLineTestimonialTranslate projectTranslatebe = new MTimeLineTestimonialTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Session = request.Session;

            int Val = 0;

            projectTranslatebe.TimeLineTestId = request.TimeLineTestimonialTranslate.TimeLineTestId;
            projectTranslatebe.LanguageId = request.TimeLineTestimonialTranslate.LanguageId;

            projectTranslatebe = BTimeLineTestimonialTranslate.Select(projectTranslatebe, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "TimeLine Testimonial Translate");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "TimeLine Testimonial Translate");
            }

            response.TimeLineTestimonialTranslate = projectTranslatebe;

            return response;
        }

        [HttpPost]
        [Route("0/InsertTimeLineTestimonialTranslate")]
        public TimeLineTestimonialTranslateResponse InsertTimeLineTestimonialTranslate([FromBody] TimeLineTestimonialTranslateRequest request)
        {
            TimeLineTestimonialTranslateResponse response = new TimeLineTestimonialTranslateResponse();
            MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate = new MTimeLineTestimonialTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            int Val = 0;

            MTimeLineTestimonialTranslate.TimeLineTestId = request.TimeLineTestimonialTranslate.TimeLineTestId;
            MTimeLineTestimonialTranslate.LanguageId = request.TimeLineTestimonialTranslate.LanguageId;
            MTimeLineTestimonialTranslate.Testimonial = request.TimeLineTestimonialTranslate.Testimonial;

            MTimeLineTestimonialTranslate.TimeLineTestId = BTimeLineTestimonialTranslate.Insert(MTimeLineTestimonialTranslate, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTestimonialTranslate", MTimeLineTestimonialTranslate.TimeLineTestId, 1, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();
                testimonialEnt = BTimeLineTestimonial.Select(testimonialEnt, ref Val);

                Val = BUtilities.UnApproved(testimonialEnt.TimeLineId, request.Session.UserId, _MAwsEmail);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "TimeLine Testimonial Translate");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "TimeLine Testimonial Translate");
            }

            response.TimeLineTestimonialTranslate = MTimeLineTestimonialTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateTimeLineTestimonialTranslate")]
        public TimeLineTestimonialTranslateResponse UpdateTimeLineTestimonialTranslate([FromBody] TimeLineTestimonialTranslateRequest request)
        {
            TimeLineTestimonialTranslateResponse response = new TimeLineTestimonialTranslateResponse();
            MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate = new MTimeLineTestimonialTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();


            baseRequest.Session = request.Session;

            int Val = 0;

            MTimeLineTestimonialTranslate.TimeLineTestId = request.TimeLineTestimonialTranslate.TimeLineTestId;
            MTimeLineTestimonialTranslate.LanguageId = request.TimeLineTestimonialTranslate.LanguageId;
            MTimeLineTestimonialTranslate.Testimonial = request.TimeLineTestimonialTranslate.Testimonial;

            MTimeLineTestimonialTranslate.TimeLineTestId = BTimeLineTestimonialTranslate.Update(MTimeLineTestimonialTranslate, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTestimonialTranslate", MTimeLineTestimonialTranslate.TimeLineTestId, 2, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();
                testimonialEnt = BTimeLineTestimonial.Select(testimonialEnt, ref Val);

                Val = BUtilities.UnApproved(testimonialEnt.TimeLineId, request.Session.UserId, _MAwsEmail);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "TimeLine Testimonial Translate");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine Testimonial Translate");
            }

            response.TimeLineTestimonialTranslate = MTimeLineTestimonialTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteTimeLineTestimonialTranslate")]
        public TimeLineTestimonialTranslateResponse DeleteTimeLineTestimonialTranslate([FromBody] TimeLineTestimonialTranslateRequest request)
        {
            TimeLineTestimonialTranslateResponse response = new TimeLineTestimonialTranslateResponse();
            MTimeLineTestimonialTranslate projectTranslatebe = new MTimeLineTestimonialTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            int Val = 0;

            projectTranslatebe.TimeLineTestId = request.TimeLineTestimonialTranslate.TimeLineTestId;
            projectTranslatebe.LanguageId = request.TimeLineTestimonialTranslate.LanguageId;

            projectTranslatebe.TimeLineTestId = BTimeLineTestimonialTranslate.Delete(projectTranslatebe, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTestimonialTranslate", projectTranslatebe.TimeLineTestId, 3, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "TimeLine Testimonial Translate");
            }

            response.TimeLineTestimonialTranslate = projectTranslatebe;

            return response;
        }

    }

}