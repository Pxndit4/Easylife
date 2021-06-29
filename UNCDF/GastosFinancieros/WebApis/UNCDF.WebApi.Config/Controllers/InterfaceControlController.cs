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
    public class InterfaceControlController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("0/GetInterfaceControls")]
        public InterfaceControlsResponse GetInterfaceControls([FromBody] InterfaceControlRequest request)
        {
            InterfaceControlsResponse response = new InterfaceControlsResponse();
            MInterfaceControl interfaceControl = new MInterfaceControl();
            List<MInterfaceControl> interfaceControls = new List<MInterfaceControl>();

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
            interfaceControl.InterfaceId = request.MInterfaceControl.InterfaceId;

            int Val = 0;

            interfaceControls = BInterfaceControl.List(interfaceControl, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Interface Controls");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Interface Controls");
            }

            response.InterfaceControls = interfaceControls.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetInterfaceControl")]
        public InterfaceControlResponse GetInterfaceControl([FromBody] InterfaceControlRequest request)
        {
            InterfaceControlResponse response = new InterfaceControlResponse();
            MInterfaceControl interfaceControl = new MInterfaceControl();

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

            interfaceControl.InterfaceControlId = request.MInterfaceControl.InterfaceControlId;

            interfaceControl = BInterfaceControl.Select(interfaceControl, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Interface Control");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Interface Control");
            }

            response.InterfaceControl = interfaceControl;

            return response;
        }

        [HttpPost]
        [Route("0/InsertInterfaceControl")]
        public InterfaceControlResponse InsertInterfaceControl([FromBody] InterfaceControlRequest request)
        {
            InterfaceControlResponse response = new InterfaceControlResponse();
            MInterfaceControl interfaceControl = new MInterfaceControl();

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

            interfaceControl.InterfaceId = request.MInterfaceControl.InterfaceId;
            interfaceControl.ControlName = request.MInterfaceControl.ControlName;
            interfaceControl.Description = request.MInterfaceControl.Description;
            interfaceControl.DescriptionControl = request.MInterfaceControl.DescriptionControl;

            interfaceControl.InterfaceControlId = BInterfaceControl.Insert(interfaceControl, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("InterfaceControl", interfaceControl.InterfaceControlId, 1, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Interface Control");
            }

            response.InterfaceControl = interfaceControl;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateInterfaceControl")]
        public InterfaceControlResponse UpdateInterfaceControl([FromBody] InterfaceControlRequest request)
        {
            InterfaceControlResponse response = new InterfaceControlResponse();
            MInterfaceControl interfaceControl = new MInterfaceControl();

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

            interfaceControl.InterfaceId = request.MInterfaceControl.InterfaceId;
            interfaceControl.InterfaceControlId = request.MInterfaceControl.InterfaceControlId;
            interfaceControl.ControlName = request.MInterfaceControl.ControlName;
            interfaceControl.Description = request.MInterfaceControl.Description;
            interfaceControl.DescriptionControl = request.MInterfaceControl.DescriptionControl;

            interfaceControl.InterfaceControlId = BInterfaceControl.Update(interfaceControl, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("InterfaceControl", interfaceControl.InterfaceControlId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Interface Control");
            }

            response.InterfaceControl = interfaceControl;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteInterfaceControl")]
        public InterfaceControlResponse DeleteInterfaceControl([FromBody] InterfaceControlRequest request)
        {
            InterfaceControlResponse response = new InterfaceControlResponse();
            MInterfaceControl interfaceControl = new MInterfaceControl();

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

            interfaceControl.InterfaceControlId = request.MInterfaceControl.InterfaceControlId;

            interfaceControl.InterfaceControlId = BInterfaceControl.Delete(interfaceControl, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("InterfaceControl", interfaceControl.InterfaceControlId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Interface Control");
            }

            response.InterfaceControl = interfaceControl;

            return response;
        }
    }
}
