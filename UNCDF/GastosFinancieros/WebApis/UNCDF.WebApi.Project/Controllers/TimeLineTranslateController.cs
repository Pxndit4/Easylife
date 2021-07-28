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
    public class TimeLineTranslateController : Controller
    {

        // POST api/values
        [HttpPost]
        [Route("0/GetTimeLineTranslates")]
        public TimeLineTranslatesResponse GetTimeLineTranslates([FromBody] TimeLineTranslateRequest request)
        {
            TimeLineTranslatesResponse response = new TimeLineTranslatesResponse();
            List<MTimeLineTranslate> TimeLineTranslateList = new List<MTimeLineTranslate>();
            MTimeLineTranslate MTimeLineTranslate = new MTimeLineTranslate();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MTimeLineTranslate.TimeLineId = request.TimeLineTranslateBE.TimeLineId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;
            int Val = 0;

            TimeLineTranslateList = BTimeLineTranslate.Lis(MTimeLineTranslate, ref Val);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.TimeLineTranslates = TimeLineTranslateList.ToArray();


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLine Translates");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLine Translates");
            }

            return response;
        }


        [HttpPost]
        [Route("0/GetTimeLineTranslate")]
        public TimeLineTranslateResponse GetTimeLineTranslate([FromBody] TimeLineTranslateRequest request)
        {
            TimeLineTranslateResponse response = new TimeLineTranslateResponse();
            MTimeLineTranslate projectTranslatebe = new MTimeLineTranslate();

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

            projectTranslatebe.TimeLineId = request.TimeLineTranslateBE.TimeLineId;
            projectTranslatebe.LanguageId = request.TimeLineTranslateBE.LanguageId;

            projectTranslatebe = BTimeLineTranslate.Select(projectTranslatebe, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "TimeLine Translate");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "TimeLine Translate");
            }

            response.TimeLineTranslate = projectTranslatebe;

            return response;
        }

        [HttpPost]
        [Route("0/InsertTimeLineTranslate")]
        public TimeLineTranslateResponse InsertTimeLineTranslate([FromBody] TimeLineTranslateRequest request)
        {
            TimeLineTranslateResponse response = new TimeLineTranslateResponse();
            MTimeLineTranslate MTimeLineTranslate = new MTimeLineTranslate();

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

            MTimeLineTranslate.TimeLineId = request.TimeLineTranslateBE.TimeLineId;
            MTimeLineTranslate.LanguageId = request.TimeLineTranslateBE.LanguageId;
            MTimeLineTranslate.Title = request.TimeLineTranslateBE.Title;
            MTimeLineTranslate.Description = request.TimeLineTranslateBE.Description;

            MTimeLineTranslate.TimeLineId = BTimeLineTranslate.Insert(MTimeLineTranslate, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTranslate", MTimeLineTranslate.TimeLineId, 1, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "TimeLine Translate");
            }

            response.TimeLineTranslate = MTimeLineTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateTimeLineTranslate")]
        public TimeLineTranslateResponse UpdateTimeLineTranslate([FromBody] TimeLineTranslateRequest request)
        {
            TimeLineTranslateResponse response = new TimeLineTranslateResponse();
            MTimeLineTranslate MTimeLineTranslate = new MTimeLineTranslate();

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

            MTimeLineTranslate.TimeLineId = request.TimeLineTranslateBE.TimeLineId;
            MTimeLineTranslate.LanguageId = request.TimeLineTranslateBE.LanguageId;
            MTimeLineTranslate.Title = request.TimeLineTranslateBE.Title;
            MTimeLineTranslate.Description = request.TimeLineTranslateBE.Description;

            MTimeLineTranslate.TimeLineId = BTimeLineTranslate.Update(MTimeLineTranslate, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTranslate", MTimeLineTranslate.TimeLineId, 2, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine Translate");
            }

            response.TimeLineTranslate = MTimeLineTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteTimeLineTranslate")]
        public TimeLineTranslateResponse DeleteTimeLineTranslate([FromBody] TimeLineTranslateRequest request)
        {
            TimeLineTranslateResponse response = new TimeLineTranslateResponse();
            MTimeLineTranslate projectTranslatebe = new MTimeLineTranslate();

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

            projectTranslatebe.TimeLineId = request.TimeLineTranslateBE.TimeLineId;
            projectTranslatebe.LanguageId = request.TimeLineTranslateBE.LanguageId;

            projectTranslatebe.TimeLineId = BTimeLineTranslate.Delete(projectTranslatebe, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineTranslate", projectTranslatebe.TimeLineId, 3, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "TimeLine Translate");
            }

            response.TimeLineTranslate = projectTranslatebe;

            return response;
        }

    }

}