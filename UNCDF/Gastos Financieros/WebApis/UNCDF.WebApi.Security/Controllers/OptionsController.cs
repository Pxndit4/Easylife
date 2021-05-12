using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Models;
using UNCDF.Layers.Business;

namespace UNCDF.WebApi.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class OptionsController : Controller
    {
        [HttpPost]
        [Route("0/GetOptions")]
        public OptionsResponse GetOptions([FromBody] OptionsRequest request)
        {
            OptionsResponse response = new OptionsResponse();
            MOptions option = new MOptions();
            List<MOptions> options = new List<MOptions>();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                option.ProfileId = request.Options.ProfileId;

                int Val = 0;

                options = BOptions.Lis(option, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "options");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "options");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
           
            response.Options = options.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetOptionsByProfile")]
        public OptionsResponse GetOptionsByProfile([FromBody] OptionsRequest request)
        {
            OptionsResponse response = new OptionsResponse();
            MOptions option = new MOptions();
            List<MOptions> options = new List<MOptions>();

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                option.ProfileId = request.Options.ProfileId;

                int Val = 0;

                options = BOptions.Sel(option, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "options");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "options");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.Options = options.ToArray();

            return response;
        }
    }
}
