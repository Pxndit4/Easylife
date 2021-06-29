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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class InterfaceControlTranslateController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("0/GetInterfaceControlTranslates")]
        public InterfaceControlTranslatesResponse GetInterfaceControlTranslates([FromBody] InterfaceControlTranslateRequest request)
        {
            InterfaceControlTranslatesResponse response = new InterfaceControlTranslatesResponse();
            List<MInterfaceControlTranslate> interfaceControlTranslateList = new List<MInterfaceControlTranslate>();
            MInterfaceControlTranslate MInterfaceControlTranslate = new MInterfaceControlTranslate();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MInterfaceControlTranslate.InterfaceControlId = request.MInterfaceControlTranslate.InterfaceControlId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;
            int Val = 0;

            interfaceControlTranslateList = BInterfaceControlTranslate.List(MInterfaceControlTranslate, ref Val);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.InterfaceControlTranslates = interfaceControlTranslateList.ToArray();


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Interface Control Translates");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Interface Control Translates");
            }

            return response;
        }


        [HttpPost]
        [Route("0/GetInterfaceControlTranslate")]
        public InterfaceControlTranslateResponse GetInterfaceControlTranslate([FromBody] InterfaceControlTranslateRequest request)
        {
            InterfaceControlTranslateResponse response = new InterfaceControlTranslateResponse();
            MInterfaceControlTranslate interfacebe = new MInterfaceControlTranslate();

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

            interfacebe.InterfaceControlId = request.MInterfaceControlTranslate.InterfaceControlId;
            interfacebe.LanguageId = request.MInterfaceControlTranslate.LanguageId;

            interfacebe = BInterfaceControlTranslate.Select(interfacebe, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Interface Control Translate");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Interface Control Translate");
            }

            response.InterfaceControlTranslate = interfacebe;

            return response;
        }

        [HttpPost]
        [Route("0/InsertInterfaceControlTranslate")]
        public InterfaceControlTranslateResponse InsertInterfaceControlTranslate([FromBody] InterfaceControlTranslateRequest request)
        {
            InterfaceControlTranslateResponse response = new InterfaceControlTranslateResponse();
            MInterfaceControlTranslate MInterfaceControlTranslate = new MInterfaceControlTranslate();

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

            MInterfaceControlTranslate.InterfaceControlId = request.MInterfaceControlTranslate.InterfaceControlId;
            MInterfaceControlTranslate.LanguageId = request.MInterfaceControlTranslate.LanguageId;
            MInterfaceControlTranslate.Description = request.MInterfaceControlTranslate.Description;


            MInterfaceControlTranslate.InterfaceControlId = BInterfaceControlTranslate.Insert(MInterfaceControlTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("InterfaceControlTranslate", MInterfaceControlTranslate.InterfaceControlId, 1, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Interface Control Translate");
            }

            response.InterfaceControlTranslate = MInterfaceControlTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateInterfaceControlTranslate")]
        public InterfaceControlTranslateResponse UpdateInterfaceControlTranslate([FromBody] InterfaceControlTranslateRequest request)
        {
            InterfaceControlTranslateResponse response = new InterfaceControlTranslateResponse();
            MInterfaceControlTranslate MInterfaceControlTranslate = new MInterfaceControlTranslate();

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

            MInterfaceControlTranslate.InterfaceControlId = request.MInterfaceControlTranslate.InterfaceControlId;
            MInterfaceControlTranslate.LanguageId = request.MInterfaceControlTranslate.LanguageId;
            MInterfaceControlTranslate.Description = request.MInterfaceControlTranslate.Description;


            MInterfaceControlTranslate.InterfaceControlId = BInterfaceControlTranslate.Update(MInterfaceControlTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("InterfaceControlTranslate", MInterfaceControlTranslate.InterfaceControlId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Interface Control Translate");
            }

            response.InterfaceControlTranslate = MInterfaceControlTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteInterfaceControlTranslate")]
        public InterfaceControlTranslateResponse DeleteInterfaceControlTranslate([FromBody] InterfaceControlTranslateRequest request)
        {
            InterfaceControlTranslateResponse response = new InterfaceControlTranslateResponse();
            MInterfaceControlTranslate interfacebe = new MInterfaceControlTranslate();

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

            interfacebe.InterfaceControlId = request.MInterfaceControlTranslate.InterfaceControlId;
            interfacebe.LanguageId = request.MInterfaceControlTranslate.LanguageId;

            interfacebe.InterfaceControlId = BInterfaceControlTranslate.Delete(interfacebe, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("InterfaceControlTranslate", interfacebe.InterfaceControlId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Interface Control Translate");
            }

            response.InterfaceControlTranslate = interfacebe;

            return response;
        }
    }
}
