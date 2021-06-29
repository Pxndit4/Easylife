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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class InterfaceController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("0/GetInterfaces")]
        public InterfaceResponse GetInterfaces([FromBody] InterfaceRequest request)
        {
            InterfaceResponse response = new InterfaceResponse();
            List<MInterface> interfaceList = new List<MInterface>();
            MInterface MInterface = new MInterface();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MInterface.TypeId = request.Interface.TypeId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            interfaceList = BInterface.List(MInterface, baseRequest);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.Interfaces = interfaceList.ToArray();

            return response;
        }


        [HttpPost]
        [Route("0/GetInterface")]
        public MInterfaceResponse GetInterface([FromBody] InterfaceRequest request)
        {
            MInterfaceResponse response = new MInterfaceResponse();
            MInterface MInterface = new MInterface();

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

            MInterface.InterfaceId = request.Interface.InterfaceId;

            MInterface = BInterface.Select(MInterface, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Interface");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Interface");
            }

            response.Interface = MInterface;

            return response;
        }

        [HttpPost]
        [Route("0/InsertInterface")]
        public MInterfaceResponse InsertInterface([FromBody] InterfaceRequest request)
        {
            MInterfaceResponse response = new MInterfaceResponse();
            MInterface MInterface = new MInterface();

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

            MInterface.InterfaceName = request.Interface.InterfaceName;
            MInterface.Description = request.Interface.Description;
            MInterface.TypeId = request.Interface.TypeId;
            MInterface.Status = request.Interface.Status;


            MInterface.InterfaceId = BInterface.Insert(MInterface, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Interface", MInterface.InterfaceId, 1, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Interface");
            }

            response.Interface = MInterface;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateInterface")]
        public MInterfaceResponse UpdateInterface([FromBody] InterfaceRequest request)
        {
            MInterfaceResponse response = new MInterfaceResponse();
            MInterface MInterface = new MInterface();

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

            MInterface.InterfaceId = request.Interface.InterfaceId;
            MInterface.InterfaceName = request.Interface.InterfaceName;
            MInterface.Description = request.Interface.Description;
            MInterface.TypeId = request.Interface.TypeId;
            MInterface.Status = request.Interface.Status;

            MInterface.InterfaceId = BInterface.Update(MInterface, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Interface", MInterface.InterfaceId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Interface");
            }

            response.Interface = MInterface;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteInterface")]
        public MInterfaceResponse DeleteInterface([FromBody] InterfaceRequest request)
        {
            MInterfaceResponse response = new MInterfaceResponse();
            MInterface MInterface = new MInterface();

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

            MInterface.InterfaceId = request.Interface.InterfaceId;

            MInterface.InterfaceId = BInterface.Delete(MInterface, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Interface", MInterface.InterfaceId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Interface");
            }

            response.Interface = MInterface;

            return response;
        }

        [HttpPost]
        [Route("0/FilterInterfaces")]
        public InterfaceResponse FilterInterfaces([FromBody] InterfaceRequest request)
        {
            InterfaceResponse response = new InterfaceResponse();
            MInterface MInterface = new MInterface();
            List<MInterface> interfaces = new List<MInterface>();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            //baseRequest.Interface = request.Interface;
            //baseRequest.Session = request.Session;

            int Val = 0;

            MInterface.InterfaceName = request.Interface.InterfaceName;
            MInterface.TypeId = request.Interface.TypeId;
            MInterface.Status = request.Interface.Status;

            interfaces = BInterface.Filter(MInterface, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Interfaces");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Interfaces");
            }

            response.Interfaces = interfaces.ToArray();

            return response;
        }
    }
}
