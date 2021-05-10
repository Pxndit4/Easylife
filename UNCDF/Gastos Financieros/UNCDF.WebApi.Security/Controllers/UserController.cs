using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Models;
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

                    MParameter parameterBE = new MParameter();
                    List<MParameter> parameterBEs = new List<MParameter>();

                    parameterBE.Code = "MAIL_PASSUSER";
                    parameterBEs = BParameter.List(parameterBE, ref Val);

                    _MAwsEmail.Subject = "UNITLIFE - User generation";
                    _MAwsEmail.Message = parameterBEs[0].Valor1.Replace("[Password]", UEncrypt.Decrypt(user.Password));
                    _MAwsEmail.ToEmail = user.User;

                    BAwsSDK.SendEmailAsync(_MAwsEmail);
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

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string AccesKeyEmailE = UEncrypt.Encrypt("AKIAZHFFPJQQNKQDOWOU");
            string SecrectKeyEmailE = UEncrypt.Encrypt("FvQ42nq+QotCo5D4UHorcnJcx1kkhf83fu0S6ONo");

            string AccesKeyS3E = UEncrypt.Encrypt("AKIAUNG53NSNKWMQIBG6");
            string SecrectKeyS3E = UEncrypt.Encrypt("eEWajEPIKkui8/JToPCH/hReb9tyGjqvZXVmBwl2");
            string SecrectKeyBucketE = UEncrypt.Encrypt("uncdfbucket");

            string DataSourceE = UEncrypt.Encrypt("motivainstancia01.caefyxytr0id.us-west-2.rds.amazonaws.com,1433");
            string UserIDE = UEncrypt.Encrypt("MotivaSQL01");
            string PasswordE = UEncrypt.Encrypt("Passw0rd");


            string AccesKeyEmailD = UEncrypt.Decrypt(AccesKeyEmailE);
            string SecrectKeyEmailD = UEncrypt.Decrypt(SecrectKeyEmailE);

            string AccesKeyS3D = UEncrypt.Decrypt(AccesKeyS3E);
            string SecrectKeyS3D = UEncrypt.Decrypt(SecrectKeyS3E);
            string SecrectKeyBucketD = UEncrypt.Decrypt(SecrectKeyBucketE);

            string DataSourceD = UEncrypt.Decrypt(DataSourceE);
            string UserIDD = UEncrypt.Decrypt(UserIDE);
            string PasswordD = UEncrypt.Decrypt(PasswordE);

            _MAwsEmail.ToEmail = "giancarlo.tueros@gmail.com";
            _MAwsEmail.Subject = "Correo de prueba";
            _MAwsEmail.Message = "Correo de prueba";

            BAwsSDK.SendEmailAsync(_MAwsEmail);

            return new string[] { "AccesKeyEmailE: " + AccesKeyEmailE,
                                  "SecrectKeyEmailE: " +  SecrectKeyEmailE,
                                  "AccesKeyS3E: " +  AccesKeyS3E,
                                  "SecrectKeyS3E: " +  SecrectKeyS3E,
                                  "SecrectKeyBucketE: " +  SecrectKeyBucketE,
                                  "AccesKeyEmailD: " + AccesKeyEmailD,
                                  "SecrectKeyEmailD: " +  SecrectKeyEmailD,
                                  "AccesKeyS3D: " +  AccesKeyS3D,
                                  "SecrectKeyS3D: " +  SecrectKeyS3D,
                                  "SecrectKeyBucketD: " +  SecrectKeyBucketD,

                                  "DataSourceE: " +  DataSourceE,
                                  "UserIDE: " + UserIDE,
                                  "PasswordE: " +  PasswordE,
                                  "DataSourceD: " +  DataSourceD,
                                  "UserIDD: " +  UserIDD,
                                  "PasswordD: " +  PasswordD,
            };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
