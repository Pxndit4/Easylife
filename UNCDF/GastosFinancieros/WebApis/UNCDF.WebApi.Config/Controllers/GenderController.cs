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
        [Route("0/GetGenders")]

        public GendersResponse GetGenders([FromBody] GenderRequest request)
        {
            GendersResponse response = new GendersResponse();
            MGender genderBE = new MGender();
            List<MGender> genderBELis = new List<MGender>();

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

            genderBE.Description = request.GenderBE.Description;
            genderBE.Status = request.GenderBE.Status;

            genderBELis = BGender.List(genderBE, ref Val, Language);

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

            response.Genders = genderBELis.ToArray();

            return response;
        }
    }
}
