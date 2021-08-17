using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;
using UNCDF.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IHostingEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public UserController(IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/InsertUser")]
        public UserResponse InsertUser([FromBody] UserRequest request) 
        {
            UserResponse response = new UserResponse();
            MUser user = new MUser();

            try
            {
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

                user.Type = request.User.Type;
                user.User = request.User.User;
                user.Name = request.User.Name;
                user.Status = request.User.Status;
                user.Password = UEncrypt.Encrypt(UCommon.RandomNumber(1000, 9999).ToString());
                user.Token = UCommon.GetTokem();

                int Val = 0;

                BUser.Insert(user, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;

                    SendEmail(user.Password, user.User);
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "User");
                }                
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }

            response.User = user;

            return response;
        }

        [HttpPost]
        [Route("0/UpdatetUser")]
        public UserResponse UpdatetUser([FromBody] UserRequest request)
        {
            UserResponse response = new UserResponse();
            MUser user = new MUser();

            try
            {
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

                user.UserId = request.User.UserId;
                user.User = request.User.User;
                user.Name = request.User.Name;
                user.Status = request.User.Status;

                int Val = 0;

                BUser.Update(user, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorUpdate, "User");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
            

            response.User = user;

            return response;
        }

        [HttpPost]
        [Route("0/GetUser")]
        public UserResponse GetUser([FromBody] UserRequest request)
        {
            UserResponse response = new UserResponse();
            MUser user = new MUser();

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

            user.UserId = request.User.UserId;

            int Val = 0;

            user = BUser.Sel(user, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "User");
            }
            else if (Val.Equals(1))
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "User");
            }

            response.User = user;

            return response;
        }




        [HttpPost]
        [Route("0/GetUsers")]
        public UsersResponse GetUsers([FromBody] UserRequest request)
        {
            UsersResponse response = new UsersResponse();
            MUser user = new MUser();
            List<MUser> users = new List<MUser>();
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

                user.User = request.User.User;
                user.Name = request.User.Name;

                int Val = 0;

                users = BUser.Lis(user, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "Users");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "Users");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.Users = users.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/ChangePassword")]
        public UserResponse ChangePassword([FromBody] UserRequest request)
        {
            UserResponse response = new UserResponse();
            MUser user = new MUser();

            BaseRequest baseRequest = new BaseRequest();

            try
            {
                baseRequest.Session = request.Session;

                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                user.UserId = request.User.UserId;
                user.Password = UEncrypt.Encrypt(UCommon.RandomNumber(1000, 9999).ToString());
                user.Token = UCommon.GetTokem();

                string Password = user.Password;
                int Val = 0;

                BUser.ChangePassword(user, ref Val);

                if (Val.Equals(0))
                {
                    user = BUser.Sel(user, ref Val);

                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;

                    SendEmail(Password, user.User);
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = "Failed to change password.";
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }            

            response.User = user;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteUser")]
        public UserResponse DeleteUser([FromBody] UserRequest request)
        {
            UserResponse response = new UserResponse();

            BaseRequest baseRequest = new BaseRequest();
            MUser user = new MUser();

            try
            {
                baseRequest.Language = request.Language;
                baseRequest.Session = request.Session;

                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/
              
                user.UserId = request.User.UserId;

                int Val = 0;

                user.UserId = BUser.Delete(user, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorDelete, "User");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }
            

            response.User = user;

            return response;
        }

        [HttpPost]
        [Route("0/LoginUser")]
        public UserResponse LoginUser([FromBody] UserRequest request)
        {
            UserResponse response = new UserResponse();
            MUser user = new MUser();

            BaseRequest baseRequest = new BaseRequest();

            try
            {
                baseRequest.Session = request.Session;

                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                user.User = request.User.User;
                user.Password = UEncrypt.Encrypt(request.User.Password);

                int Val = 0;

                user = BUser.Login(user, ref Val);

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorSelect, "User");
                }
                else if (Val.Equals(1))
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = "User does not exist";
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }

            response.User = user;

            return response;
        }

        private void SendEmail(string Password, string User) {
            MParameter parameterBE = new MParameter();
            List<MParameter> parameterBEs = new List<MParameter>();

            int Val = 0;

            parameterBE.Code = "MAIL_PASSUSER";
            parameterBEs = BParameter.List(parameterBE, ref Val);

            _MAwsEmail.Subject = "UNCDF - User generation";
            _MAwsEmail.Message = parameterBEs[0].Valor1.Replace("[Password]", UEncrypt.Decrypt(Password));
            _MAwsEmail.ToEmail = User;

            BAwsSDK.SendEmailAsync(_MAwsEmail);
        }
        
    }
}
