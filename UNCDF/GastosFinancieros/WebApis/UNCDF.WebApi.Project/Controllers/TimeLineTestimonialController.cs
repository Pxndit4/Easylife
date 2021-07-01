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
    [EnableCors("AllowMyOrigin")]
    public class TimeLineTestimonialController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;


        public TimeLineTestimonialController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }


        [HttpPost]
        [Route("0/InsertTimeLineTestimonial")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public TimeLineTestimonialResponse InsertTimeLineTestimonial([FromBody] TimeLineTestimonialRequest request)
        {
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            string webRoot = _env.ContentRootPath;
            string rootPath = _appSettings.Value.rootPath;
            string TimeLineTestimonialPath = _appSettings.Value.TimeLineTestimonialPath;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            testimonialEnt.TimeLineId = request.Testimonial.TimeLineId;
            testimonialEnt.Name = request.Testimonial.Name;
            testimonialEnt.Testimonial = request.Testimonial.Testimonial;
            testimonialEnt.Photo = request.Testimonial.Photo;

            int Val = 0;

            testimonialEnt.TimeLineTestId = BTimeLineTestimonial.Insert(testimonialEnt, ref Val);

            if (request.Testimonial.FileByte != null)
            {
                //Requered Update File
                string path = TimeLineTestimonialPath + "/" + testimonialEnt.TimeLineTestId.ToString() + request.Testimonial.Ext;
                testimonialEnt.Photo = path;
                BTimeLineTestimonial.Update(testimonialEnt, ref Val);

                byte[] File = request.Testimonial.FileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + path;

                if (!Directory.Exists(pathAbs + rootPath + TimeLineTestimonialPath)) Directory.CreateDirectory(pathAbs + rootPath + TimeLineTestimonialPath);

                if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                System.IO.File.WriteAllBytes(pathSave, File);

                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, TimeLineTestimonialPath, testimonialEnt.TimeLineTestId.ToString() + request.Testimonial.Ext))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "TimeLine Testimonial");
                }

                System.IO.File.Delete(pathSave);
            }

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTestimonial", testimonialEnt.TimeLineTestId, 1, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "TimeLine Testimonial");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "TimeLine Testimonial");
            }

            response.Testimonial = testimonialEnt;

            return response;
        }


        [HttpPost]
        [Route("0/UpdateTimeLineTestimonial")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public TimeLineTestimonialResponse UpdateTimeLineTestimonial([FromBody] TimeLineTestimonialRequest request)
        {
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            string webRoot = _env.ContentRootPath;
            string rootPath = _appSettings.Value.rootPath;
            string TimeLineTestimonialPath = _appSettings.Value.TimeLineTestimonialPath;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            testimonialEnt.TimeLineTestId = request.Testimonial.TimeLineTestId;
            testimonialEnt.TimeLineId = request.Testimonial.TimeLineId;
            testimonialEnt.Name = request.Testimonial.Name;
            testimonialEnt.Testimonial = request.Testimonial.Testimonial;

            if (request.Testimonial.FileByte != null)
            {
                testimonialEnt.Photo = TimeLineTestimonialPath + "/" + request.Testimonial.TimeLineTestId.ToString() + request.Testimonial.Ext;
            }
            else
            {
                testimonialEnt.Photo = TimeLineTestimonialPath + "/" + request.Testimonial.Photo;
            }

            int Val = 0;

            testimonialEnt.TimeLineTestId = BTimeLineTestimonial.Update(testimonialEnt, ref Val);

            if (request.Testimonial.FileByte != null)
            {
                //Requered Update File
                string path = TimeLineTestimonialPath + "/" + testimonialEnt.TimeLineTestId.ToString() + request.Testimonial.Ext;
                testimonialEnt.Photo = path;
                // BTimeLineTestimonial.Update(testimonialEnt, ref Val);

                byte[] File = request.Testimonial.FileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + path;

                if (!Directory.Exists(pathAbs + rootPath + TimeLineTestimonialPath)) Directory.CreateDirectory(pathAbs + rootPath + TimeLineTestimonialPath);

                if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                System.IO.File.WriteAllBytes(pathSave, File);

                if (!BAwsSDK.UploadS3(_MAwsS3,pathSave, TimeLineTestimonialPath, testimonialEnt.TimeLineTestId.ToString() + request.Testimonial.Ext))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "TimeLine Testimonial");
                }

                System.IO.File.Delete(pathSave);
            }

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTestimonial", testimonialEnt.TimeLineTestId, 2, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorUpdate, "TimeLine Testimonial");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine Testimonial");
            }

            response.Testimonial = testimonialEnt;

            return response;
        }

        [HttpPost]
        [Route("0/GetTimeLineTestimonial")]
        public TimeLineTestimonialResponse GetTimeLineTestimonial([FromBody] TimeLineTestimonialRequest request)
        {
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();
            string TimeLineTestimonialPath = _appSettings.Value.TimeLineTestimonialPath + "/";

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            testimonialEnt.TimeLineTestId = request.Testimonial.TimeLineTestId;

            int Val = 0;

            testimonialEnt = BTimeLineTestimonial.Select(testimonialEnt, ref Val);
            testimonialEnt.Photo = testimonialEnt.Photo.Replace(TimeLineTestimonialPath, string.Empty);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(1))
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "TimeLine Testimonial");
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "TimeLine Testimonial");
            }

            response.Testimonial = testimonialEnt;

            return response;
        }


        [HttpPost]
        [Route("0/GetTimeLineTestimonials")]
        public TimeLineTestimonialsResponse GetTimeLineTestimonials([FromBody] TimeLineTestimonialRequest request)
        {
            TimeLineTestimonialsResponse response = new TimeLineTestimonialsResponse();
            List<MTimeLineTestimonial> testimonialEnts = new List<MTimeLineTestimonial>();
            BaseRequest baseRequest = new BaseRequest();
            string TimeLineTestimonialPath = _appSettings.Value.TimeLineTestimonialPath + "/";

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();

            testimonialEnt.TimeLineId = request.Testimonial.TimeLineId;
            testimonialEnt.Name = request.Testimonial.Name;

            int Val = 0;

            //testimonialEnts = BTimeLineTestimonial.Filter(testimonialEnt, ref Val);

            //timeLineMultimedias = TimeLineMultimediaBN.Filter(timeLineMultimedia, ref Val);
            testimonialEnts = BTimeLineTestimonial.Filter(testimonialEnt, ref Val).Select(x => new MTimeLineTestimonial
            {
                TimeLineTestId = x.TimeLineTestId,
                TimeLineId = x.TimeLineId,
                Name = x.Name,
                Photo = x.Photo,//.Replace(TimeLineTestimonialPath, string.Empty),
                Testimonial = x.Testimonial

            }).ToList();

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLine Testimonial");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLine Testimonial");
            }

            response.Testimonials = testimonialEnts.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/DeleteTimeLineTestimonial")]
        public TimeLineTestimonialResponse DeleteTimeLineTestimonial([FromBody] TimeLineTestimonialRequest request)
        {
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            MTimeLineTestimonial testimonialEnt = new MTimeLineTestimonial();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            testimonialEnt.TimeLineTestId = request.Testimonial.TimeLineTestId;

            int Val = 0;

            testimonialEnt.TimeLineTestId = BTimeLineTestimonial.Delete(testimonialEnt, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTestimonial", testimonialEnt.TimeLineTestId, 3, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "TimeLine Testimonial");
            }

            response.Testimonial = testimonialEnt;

            return response;
        }


    }
}