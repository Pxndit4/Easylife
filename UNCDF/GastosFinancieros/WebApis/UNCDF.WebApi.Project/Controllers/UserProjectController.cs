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
    public class UserProjectController : Controller
    {

        [HttpPost]
        [Route("0/RegisterUsersProfile")]
        public UserProjectResponse RegisterUsersProfile([FromBody] UserProjectRequest request)
        {
            UserProjectResponse response = new UserProjectResponse();
            MUserProject profile = new MUserProject();

            BaseRequest baseRequest = new BaseRequest();

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

                profile.ProjectId = request.UserProject.ProjectId;
                profile.UserId = request.UserProject.UserId;

                int Val = 0;

                Val = BUserProject.Insert(profile);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "Users Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }

            response.UserProject = profile;

            return response;
        }


        [HttpPost]
        [Route("0/DeleteUserProject")]
        public UserProjectResponse DeleteUserProject([FromBody] UserProjectRequest request)
        {
            UserProjectResponse response = new UserProjectResponse();
            MUserProject profile = new MUserProject();

            BaseRequest baseRequest = new BaseRequest();

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

                profile.ProjectId = request.UserProject.ProjectId;
                profile.UserId = request.UserProject.UserId;

                int Val = 0;

                Val = BUserProject.Delete(profile);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorDelete, "Users Profile");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }

            response.UserProject = profile;

            return response;
        }

    }
}
