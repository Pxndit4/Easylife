using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class GenderTranslateController : Controller
    {
        // GET: GenderTranslate
        [HttpPost]
        [Route("0/GetGenderTranslates")]
        public GenderTranslatesResponse GetGenderTranslates([FromBody] GenderTranslateRequest request)
        {
            GenderTranslatesResponse response = new GenderTranslatesResponse();
            List<MGenderTranslate> GenderTranslateList = new List<MGenderTranslate>();
            MGenderTranslate MGenderTranslate = new MGenderTranslate();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MGenderTranslate.GenderId = request.GenderTranslateBE.GenderId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;
            int Val = 0;

            GenderTranslateList = BGenderTranslate.List(MGenderTranslate, ref Val);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.GenderTranslates = GenderTranslateList.ToArray();


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Genders");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Genders");
            }

            return response;
        }


        [HttpPost]
        [Route("0/GetGenderTranslate")]
        public GenderTranslateResponse GetGenderTranslate([FromBody] GenderTranslateRequest request)
        {
            GenderTranslateResponse response = new GenderTranslateResponse();
            MGenderTranslate genderbe = new MGenderTranslate();

            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Session = request.Session;

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            int Val = 0;

            genderbe.GenderId = request.GenderTranslateBE.GenderId;
            genderbe.LanguageId = request.GenderTranslateBE.LanguageId;

            genderbe = BGenderTranslate.Select(genderbe, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Gender");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Gender");
            }

            response.GenderTranslate = genderbe;

            return response;
        }

        [HttpPost]
        [Route("0/InsertGenderTranslate")]
        public GenderTranslateResponse InsertGenderTranslate([FromBody] GenderTranslateRequest request)
        {
            GenderTranslateResponse response = new GenderTranslateResponse();
            MGenderTranslate MGenderTranslate = new MGenderTranslate();

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            int Val = 0;

            MGenderTranslate.GenderId = request.GenderTranslateBE.GenderId;
            MGenderTranslate.LanguageId = request.GenderTranslateBE.LanguageId;
            MGenderTranslate.Description = request.GenderTranslateBE.Description;
            MGenderTranslate.Value = request.GenderTranslateBE.Value;


            MGenderTranslate.GenderId = BGenderTranslate.Insert(MGenderTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("GenderTranslate", MGenderTranslate.GenderId, 1, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Gender Translate");
            }

            response.GenderTranslate = MGenderTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateGenderTranslate")]
        public GenderTranslateResponse UpdateGenderTranslate([FromBody] GenderTranslateRequest request)
        {
            GenderTranslateResponse response = new GenderTranslateResponse();
            MGenderTranslate MGenderTranslate = new MGenderTranslate();

            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            baseRequest.Session = request.Session;

            int Val = 0;

            MGenderTranslate.GenderId = request.GenderTranslateBE.GenderId;
            MGenderTranslate.LanguageId = request.GenderTranslateBE.LanguageId;
            MGenderTranslate.Description = request.GenderTranslateBE.Description;
            MGenderTranslate.Value = request.GenderTranslateBE.Value;


            MGenderTranslate.GenderId = BGenderTranslate.Update(MGenderTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("GenderTranslate", MGenderTranslate.GenderId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Gender Translate");
            }

            response.GenderTranslate = MGenderTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteGenderTranslate")]
        public GenderTranslateResponse DeleteGenderTranslate([FromBody] GenderTranslateRequest request)
        {
            GenderTranslateResponse response = new GenderTranslateResponse();
            MGenderTranslate genderbe = new MGenderTranslate();

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            int Val = 0;

            genderbe.GenderId = request.GenderTranslateBE.GenderId;
            genderbe.LanguageId = request.GenderTranslateBE.LanguageId;

            genderbe.GenderId = BGenderTranslate.Delete(genderbe, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("GenderTranslate", genderbe.GenderId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Gender Translate");
            }

            response.GenderTranslate = genderbe;

            return response;
        }
    }
}
