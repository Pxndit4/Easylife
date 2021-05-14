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
using UNCDF.Layers.Models;
using UNCDF.Utilities;

namespace UNCDF.WebApi.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class DonorController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public DonorController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/LoginDonor")]
        public DonorResponse LoginDonor([FromBody] DonorRequest request)
        {
            DonorResponse response = new DonorResponse();
            MDonor donor = new MDonor();
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

                //donor.Cellphone = request.Donor.Cellphone; //Se comenta el logeo por Email
                donor.Email = request.Donor.Email;
                donor.Password = UEncrypt.Encrypt(request.Donor.Password);

                int CodeResult = 0;
                donor = BDonor.Login(donor, ref CodeResult);

                response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                if (CodeResult == 0)
                {
                    response.Message = Messages.Success;
                }
                else if (CodeResult == 1)
                {
                    response.Message = "The session data is invalid.";
                }
                else
                {
                    response.Message = "An error occurred when logging in";
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            response.Donor = donor;

            return response;
        }

        [HttpPost]
        [Route("0/GetDonor")]
        public DonorResponse GetDonor([FromBody] DonorRequest request)
        {
            DonorResponse response = new DonorResponse();
            MDonor donor = new MDonor();
            List<MProjectDonation> Donations = new List<MProjectDonation>();
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

                donor.DonorId = request.Donor.DonorId;
                baseRequest.Session = request.Session;

                if (BSession.ValidateSession(1, baseRequest.Session.Token, baseRequest.Session.UserId).Equals(1))
                {
                    int CodeResult = 0;
                    donor = BDonor.Select(donor, ref CodeResult);

                    response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                    Donations = BProjectDonation.List(donor, baseRequest);

                    response.DonationsCounter = Donations.Count;

                    if (CodeResult == 0)
                    {
                        response.Message = Messages.Success;
                    }
                    else if (CodeResult == 1)
                    {
                        response.Message = String.Format(Messages.NoExistsSelect, "Donor");
                    }
                    else
                    {
                        response.Message = String.Format(Messages.ErrorSelect, "Donor");
                    }
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.ApplicationTokenNoAutorize;
                }
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message;
            }

            response.Donor = donor;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteDonor")]
        public DonorResponse DeleteDonor([FromBody] DonorRequest request)
        {
            DonorResponse response = new DonorResponse();
            MDonor donor = new MDonor();

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

                donor.Cellphone = request.Donor.Cellphone;

                int CodeResult = BDonor.Delete(donor);

                response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                if (CodeResult == 0)
                {
                    response.Message = Messages.Success;
                }
                else
                {
                    response.Message = String.Format(Messages.ErrorDelete, "Donor");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            response.Donor = donor;

            return response;
        }

        [HttpPost]
        [Route("0/ValidateDonor")]
        public CreateDonorResponse ValidateDonor([FromBody] DonorRequest request)
        {
            CreateDonorResponse response = new CreateDonorResponse();
            MDonor donor = new MDonor();
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

                //donor.Cellphone = request.Donor.Cellphone;
                donor.Email = request.Donor.Email;
                donor.Password = UEncrypt.Encrypt(request.Donor.Password);

                baseRequest.Session = request.Session;

                donor.DonorId = request.Session.UserId;

                if (BSession.ValidateSession(1, baseRequest.Session.Token, baseRequest.Session.UserId).Equals(1))
                {
                    int CodeResult = BDonor.ValidateCode(donor);

                    response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                    if (CodeResult == 0)
                    {
                        response.Message = Messages.Success;
                        SendSESValidate(donor.Email);
                        donor = BDonor.Select(donor, ref CodeResult);
                    }
                    else if (CodeResult == 1)
                    {
                        response.Message = "The code entered is not valid.";
                    }
                    else if (CodeResult == 3)
                    {
                        response.Code = "1";
                        response.Message = "The Donor is already validated.";
                    }
                    else
                    {
                        response.Message = "The Donor is already validated.";
                    }
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.ApplicationTokenNoAutorize;
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            response.Donor = donor;

            return response;
        }

        [HttpPost]
        [Route("0/ResetPassword")]
        public CreateDonorResponse ResetPassword([FromBody] DonorRequest request)
        {
            CreateDonorResponse response = new CreateDonorResponse();
            MDonor donor = new MDonor();

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

                donor.Email = request.Donor.Email;

                int refval = 0;

                donor = BDonor.ValidateDonor(donor, ref refval);

                if (refval.Equals(0))
                {
                    donor.Password = UEncrypt.Encrypt(UCommon.RandomNumber(1000, 9999).ToString());
                    donor.Token = UCommon.GetTokem();

                    MCountry countryBE = new MCountry();
                    countryBE.CountryId = donor.CountryId;
                    countryBE = BCountry.Select(countryBE, ref refval);

                    if (BDonor.UpdateCode(donor) == 0)
                    {
                        if (!request.Donor.Email.Equals(""))
                        {
                            SendSES(donor.Password, donor.Email);
                        }

                        response.Code = "0";
                        response.Message = Messages.Success;
                    }
                    else
                    {
                        response.Code = "2";
                        response.Message = String.Format(Messages.ErrorInsert, "Donor");
                    }
                }
                else
                {
                    donor.Cellphone = request.Donor.Cellphone;
                    donor.Email = request.Donor.Email;
                    response.Code = "1";

                    if (!donor.Email.Equals(""))
                    {
                        response.Message = "The email entered is not registered";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            response.Donor = donor;

            return response;
        }

        [HttpPost]
        [Route("0/CreateDonor")]
        public CreateDonorResponse CreateDonor([FromBody] DonorRequest request)
        {
            CreateDonorResponse response = new CreateDonorResponse();
            MDonor donor = new MDonor();
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

                //donor.Cellphone = request.Donor.Cellphone;
                donor.Email = request.Donor.Email;
                donor.Password = UEncrypt.Encrypt(UCommon.RandomNumber(1000, 9999).ToString());
                //donor.CountryId = request.Donor.CountryId;
                donor.Token = UCommon.GetTokem();
                donor.Status = 1;

                baseRequest.Session = request.Session;

                int DonorId = 0;

                int CodeResult = BDonor.Insert(donor, baseRequest, ref DonorId);

                response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                if (CodeResult == 0)
                {
                    response.Message = Messages.Success;
                    //SendSNS(donor.Password, donor.Cellphone, countryBE.Prefix);
                    SendSES(donor.Password, donor.Email);
                }
                else if (CodeResult == 1)
                {
                    if (donor.Registered == 0)
                    {
                        if (BDonor.UpdateCode(donor) == 0)
                        {
                            donor.DonorId = DonorId;

                            response.Code = "0";
                            response.Message = Messages.Success;
                            //SendSNS(donor.Password, donor.Cellphone, countryBE.Prefix);
                            SendSES(donor.Password, donor.Email);
                        }
                        else
                        {
                            response.Message = String.Format(Messages.ErrorInsert, "Donor");
                        }
                    }
                    else
                    {
                        //response.Message = "The cell phone number and email entered are already used.";
                        response.Message = "The email entered are already used.";
                    }
                }
                else
                {
                    response.Message = String.Format(Messages.ErrorInsert, "Donor");
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }            

            response.Donor = donor;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateDonor")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public CreateDonorResponse UpdateDonor([FromBody] DonorRequest request)
        {
            CreateDonorResponse response = new CreateDonorResponse();
            MDonor donor = new MDonor();
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

                string webRoot = _env.ContentRootPath;
                string rootPath = _appSettings.Value.rootPath;
                string DonorPath = _appSettings.Value.DonorPath;

                donor.DonorId = request.Donor.DonorId;
                donor.Cellphone = request.Donor.Cellphone;
                donor.Email = request.Donor.Email;
                donor.CountryId = request.Donor.CountryId;
                donor.FirstName = request.Donor.FirstName;
                donor.LastName = request.Donor.LastName;
                donor.Gender = request.Donor.Gender;
                donor.Birthday = request.Donor.Birthday;
                donor.Address = request.Donor.Address;
                donor.Photo = DonorPath + "/" + request.Donor.Photo.Replace(Constant.S3Server, string.Empty).Replace(DonorPath + "/", string.Empty);
                donor.Status = 1;

                baseRequest.Session = request.Session;

                byte[] FileByte = request.FileByte ?? Encoding.ASCII.GetBytes("");

                if (BSession.ValidateSession(1, baseRequest.Session.Token, baseRequest.Session.UserId).Equals(1))
                {
                    if (!FileByte.Length.Equals(0))
                    {
                        try
                        {
                            donor.Photo = donor.DonorId.ToString() + request.PhotoExtension;

                            //Grabamos el archivo
                            Uri webRootUri = new Uri(webRoot);
                            string path = webRootUri.AbsolutePath + rootPath + DonorPath;

                            var pathSave = Path.Combine(path, donor.Photo);

                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                            if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                            System.IO.File.WriteAllBytes(pathSave, FileByte);

                            if (BAwsSDK.UploadS3(_MAwsS3, pathSave, DonorPath, donor.Photo))
                            {
                                response.Message = String.Format(Messages.ErrorLoadPhoto, "Donor");
                            }

                            System.IO.File.Delete(pathSave);

                            donor.Photo = DonorPath + "/" + donor.Photo;
                        }
                        catch (Exception ex)
                        {
                            response.Message = String.Format(Messages.ErrorUpdate, "Banner") + ex.Message;
                            response.Donor = donor;
                            return response;
                        }

                    }

                    int CodeResult = BDonor.Update(donor);

                    response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                    if (CodeResult == 0)
                    {
                        response.Message = Messages.Success;
                    }
                    else
                    {
                        response.Message = String.Format(Messages.ErrorUpdate, "Banner");
                    }
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.ApplicationTokenNoAutorize;
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }
            
            response.Donor = donor;
            response.Donor.Token = request.Session.Token;

            return response;
        }

        [HttpPost]
        [Route("0/ChangePasswordDonor")]
        public CreateDonorResponse ChangePasswordDonor([FromBody] DonorRequest request)
        {
            CreateDonorResponse response = new CreateDonorResponse();
            MDonor donor = new MDonor();
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

                donor.DonorId = request.Donor.DonorId;
                donor.Email = request.Donor.Email;//donor.Cellphone = request.Donor.Cellphone;
                donor.Password = UEncrypt.Encrypt(request.Donor.Password);
                donor.OldPassword = UEncrypt.Encrypt(request.Donor.OldPassword);

                baseRequest.Session = request.Session;

                if (BSession.ValidateSession(1, baseRequest.Session.Token, baseRequest.Session.UserId).Equals(1))
                {
                    int CodeResult = BDonor.ChangePassword(donor);
                    response.Code = CodeResult.ToString(); //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción

                    if (CodeResult == 0)
                    {
                        response.Message = Messages.Success;
                    }
                    else if (CodeResult == 1)
                    {
                        response.Message = "The old password entered is invalid";
                    }
                    else
                    {
                        response.Message = "An error occurred when changing the password";
                    }
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.ApplicationTokenNoAutorize;
                }
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }            

            response.Donor = donor;

            return response;
        }

        private void SendSES(string Password, string Email)
        {
            int val = 0;
            MParameter parameterBE = new MParameter();
            List<MParameter> parameterBEs = new List<MParameter>();

            parameterBE.Code = "MAIL_PASS";
            parameterBEs = BParameter.List(parameterBE, ref val);

            if (val.Equals(0))
            {
                _MAwsEmail.Subject = "UNITLIFE - User generation";
                _MAwsEmail.Message = parameterBEs[0].Valor1.Replace("[Password]", UEncrypt.Decrypt(Password));
                _MAwsEmail.ToEmail = Email;

                BAwsSDK.SendEmailAsync(_MAwsEmail);               
            }

        }

        private void SendSESValidate(string Email)
        {
            int val = 0;
            MParameter parameterBE = new MParameter();
            List<MParameter> parameterBEs = new List<MParameter>();

            parameterBE.Code = "MAIL_VALIDATE";
            parameterBEs = BParameter.List(parameterBE, ref val);

            if (val.Equals(0))
            {
                _MAwsEmail.Subject = "UNITLIFE - Registration";
                _MAwsEmail.Message = parameterBEs[0].Valor1;
                _MAwsEmail.ToEmail = Email;

                BAwsSDK.SendEmailAsync(_MAwsEmail);
            }

        }
    }
}
