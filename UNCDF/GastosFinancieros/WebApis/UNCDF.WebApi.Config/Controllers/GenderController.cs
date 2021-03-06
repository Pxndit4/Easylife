using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [EnableCors("_corsPolicy")]
    public class GenderController : ControllerBase
    {


        [HttpPost]
        [Route("0/GetGender")]
        public GenderResponse GetGender([FromBody] GenderRequest request)
        {
            GenderResponse response = new GenderResponse();
            MGender MGender = new MGender();

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

            MGender.GenderId = request.GenderBE.GenderId;

            MGender = BGender.Select(MGender, ref Val);

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

            response.Gender = MGender;

            return response;
        }

        [HttpPost]
        [Route("0/GetGenders")]

        public GendersResponse GetGenders([FromBody] GenderRequest request)
        {
            GendersResponse response = new GendersResponse();
            MGender MGender = new MGender();
            List<MGender> MGenderLis = new List<MGender>();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            string Language = "ENG";

            if (request.Language != null)
            {
                Language = request.Language;
            }
            //baseRequest.Gender = request.Gender;
            //baseRequest.Session = request.Session;

            int Val = 0;

            MGender.Description = request.GenderBE.Description;
            MGender.Status = request.GenderBE.Status;

            MGenderLis = BGender.List(MGender, ref Val, Language);

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

            response.Genders = MGenderLis.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/InsertGender")]
        public GenderResponse InsertGender([FromBody] GenderRequest request)
        {
            GenderResponse response = new GenderResponse();
            MGender MGender = new MGender();

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

            MGender.Value = request.GenderBE.Value;
            MGender.Description = request.GenderBE.Description;
            MGender.Status = request.GenderBE.Status;


            MGender.GenderId = BGender.Insert(MGender, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Gender", MGender.GenderId, 1, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Gender");
            }

            response.Gender = MGender;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateGender")]
        public GenderResponse UpdateGender([FromBody] GenderRequest request)
        {
            GenderResponse response = new GenderResponse();
            MGender MGender = new MGender();

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

            MGender.GenderId = request.GenderBE.GenderId;
            MGender.Description = request.GenderBE.Description;
            MGender.Value = request.GenderBE.Value;
            MGender.Status = request.GenderBE.Status;

            MGender.GenderId = BGender.Update(MGender, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Gender", MGender.GenderId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Gender");
            }

            response.Gender = MGender;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteGender")]
        public GenderResponse DeleteGender([FromBody] GenderRequest request)
        {
            GenderResponse response = new GenderResponse();
            MGender MGender = new MGender();

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

            MGender.GenderId = request.GenderBE.GenderId;

            MGender.GenderId = BGender.Delete(MGender, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Gender", MGender.GenderId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Gender");
            }

            response.Gender = MGender;

            return response;
        }

        

    }
}
