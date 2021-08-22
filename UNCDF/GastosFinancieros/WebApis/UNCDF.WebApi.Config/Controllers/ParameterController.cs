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
    public class ParameterController : ControllerBase
    {
        [HttpPost]
        [Route("0/GetParameters")]
        public ParametersResponse GetParameters([FromBody] ParameterRequest request)
        {
            ParametersResponse response = new ParametersResponse();
            List<MParameter> bannersBE = new List<MParameter>();
            MParameter parameterBE = new MParameter();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            parameterBE.Code = request.Parameter.Code;
            parameterBE.Description = request.Parameter.Description;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            bannersBE = BParameter.List(parameterBE, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Parameters");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Parameters");
            }

            response.Parameters = bannersBE.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/UpdateParameter")]
        public ParameterResponse UpdateParameter([FromBody] ParameterRequest request)
        {
            ParameterResponse response = new ParameterResponse();
            MParameter ent = new MParameter();

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

            ent.ParameterId = request.Parameter.ParameterId;
            ent.Code = request.Parameter.Code;
            ent.Description = request.Parameter.Description;
            ent.Valor1 = request.Parameter.Valor1;
            ent.Valor2 = request.Parameter.Valor2;
            ent.Status = request.Parameter.Status;

            int Val = 0;

            ent.ParameterId = BParameter.Update(ent, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Parameters", ent.ParameterId, 2, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Parameter");
            }

            response.Parameter = ent;

            return response;
        }

        [HttpPost]
        [Route("0/GetParameter")]
        public ParameterResponse GetParameter([FromBody] ParameterRequest request)
        {
            ParameterResponse response = new ParameterResponse();
            MParameter ent = new MParameter();

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

            ent.ParameterId = request.Parameter.ParameterId;

            int Val = 0;

            ent = BParameter.Select(ent, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(1))
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Parameter");
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Parameter");
            }

            response.Parameter = ent;

            return response;
        }

    }
}
