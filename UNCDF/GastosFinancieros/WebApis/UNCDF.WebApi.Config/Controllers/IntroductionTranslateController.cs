using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;
using UNCDF.Utilities;

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class IntroductionTranslateController : Controller
    {

        // POST api/values
        [HttpPost]
        [Route("0/GetIntroductionTranslates")]
        public IntroductionTranslatesResponse GetIntroductionTranslates([FromBody] IntroductionTranslateRequest request)
        {
            IntroductionTranslatesResponse response = new IntroductionTranslatesResponse();
            List<MIntroductionTranslate> IntroductionTranslateList = new List<MIntroductionTranslate>();
            MIntroductionTranslate MIntroductionTranslate = new MIntroductionTranslate();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MIntroductionTranslate.IntroductionId = request.MIntroductionTranslate.IntroductionId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;
            int Val = 0;

            IntroductionTranslateList = BIntroductionTranslate.Lis(MIntroductionTranslate, ref Val);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.IntroductionTranslates = IntroductionTranslateList.ToArray();


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Introduction Translates");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Introduction Translates");
            }

            return response;
        }


        [HttpPost]
        [Route("0/GetIntroductionTranslate")]
        public IntroductionTranslateResponse GetIntroductionTranslate([FromBody] IntroductionTranslateRequest request)
        {
            IntroductionTranslateResponse response = new IntroductionTranslateResponse();
            MIntroductionTranslate genderbe = new MIntroductionTranslate();

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

            genderbe.IntroductionId = request.MIntroductionTranslate.IntroductionId;
            genderbe.LanguageId = request.MIntroductionTranslate.LanguageId;

            genderbe = BIntroductionTranslate.Select(genderbe, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Introduction Translate");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Introduction Translate");
            }

            response.IntroductionTranslate = genderbe;

            return response;
        }

        [HttpPost]
        [Route("0/InsertIntroductionTranslate")]
        public IntroductionTranslateResponse InsertIntroductionTranslate([FromBody] IntroductionTranslateRequest request)
        {
            IntroductionTranslateResponse response = new IntroductionTranslateResponse();
            MIntroductionTranslate MIntroductionTranslate = new MIntroductionTranslate();

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

            MIntroductionTranslate.IntroductionId = request.MIntroductionTranslate.IntroductionId;
            MIntroductionTranslate.LanguageId = request.MIntroductionTranslate.LanguageId;
            MIntroductionTranslate.Description = request.MIntroductionTranslate.Description;
            MIntroductionTranslate.Title = request.MIntroductionTranslate.Title;


            MIntroductionTranslate.IntroductionId = BIntroductionTranslate.Insert(MIntroductionTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("IntroductionTranslate", MIntroductionTranslate.IntroductionId, 1, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Introduction Translate");
            }

            response.IntroductionTranslate = MIntroductionTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateIntroductionTranslate")]
        public IntroductionTranslateResponse UpdateIntroductionTranslate([FromBody] IntroductionTranslateRequest request)
        {
            IntroductionTranslateResponse response = new IntroductionTranslateResponse();
            MIntroductionTranslate MIntroductionTranslate = new MIntroductionTranslate();

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

            MIntroductionTranslate.IntroductionId = request.MIntroductionTranslate.IntroductionId;
            MIntroductionTranslate.LanguageId = request.MIntroductionTranslate.LanguageId;
            MIntroductionTranslate.Description = request.MIntroductionTranslate.Description;
            MIntroductionTranslate.Title = request.MIntroductionTranslate.Title;


            MIntroductionTranslate.IntroductionId = BIntroductionTranslate.Update(MIntroductionTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("IntroductionTranslate", MIntroductionTranslate.IntroductionId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Introduction Translate");
            }

            response.IntroductionTranslate = MIntroductionTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteIntroductionTranslate")]
        public IntroductionTranslateResponse DeleteIntroductionTranslate([FromBody] IntroductionTranslateRequest request)
        {
            IntroductionTranslateResponse response = new IntroductionTranslateResponse();
            MIntroductionTranslate genderbe = new MIntroductionTranslate();

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

            genderbe.IntroductionId = request.MIntroductionTranslate.IntroductionId;
            genderbe.LanguageId = request.MIntroductionTranslate.LanguageId;

            genderbe.IntroductionId = BIntroductionTranslate.Delete(genderbe, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("IntroductionTranslate", genderbe.IntroductionId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Introduction Translate");
            }

            response.IntroductionTranslate = genderbe;

            return response;
        }

    }

}