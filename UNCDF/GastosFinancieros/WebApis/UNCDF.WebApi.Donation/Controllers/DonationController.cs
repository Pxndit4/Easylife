using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;
using UNCDF.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class DonationController : Controller
    {

        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;
        private readonly IWebHostEnvironment _env;

        public DonationController(IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3,
                                    IWebHostEnvironment env)
        {
            _env = env;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        // POST api/values
        [HttpPost]
        [Route("0/DonationList")]
        [EnableCors("AllowMyOrigin")]
        public DonationsResponse DonationList([FromBody] DonationRequest request)
        {
            DonationsResponse response = new DonationsResponse();
            List<MDonation> Donations = new List<MDonation>();
            MDonation donation = new MDonation();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            donation.DonorId = request.Donation.DonorId;
            donation.OngId = request.Donation.OngId;
            donation.FirstName = request.Donation.FirstName;
            donation.LastName = request.Donation.LastName;
            donation.StartDate = request.Donation.StartDate;
            donation.EndDate = request.Donation.EndDate;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            Donations = BDonation.List(donation, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Donations");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Donations");
            }

            response.Donations = Donations.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetPaymentStripe")]
        public JsonResult GetPaymentStripe([FromBody] GetPaymentStripeRequest request)
        {
            try
            {
                var paymentIntents = new PaymentIntentService();
                var payment = paymentIntents.Get(request.PaymentId);

                JsonResult json = new JsonResult(new { status = "OK", message = payment.Status });

                if (payment.Status != "succeeded" && payment.Status != "canceled")
                {
                    var paymentCancel = paymentIntents.Cancel(request.PaymentId);

                    if (paymentCancel.StripeResponse.StatusCode.Equals("OK"))
                    {
                        int resul = BDonorStripe.Cancel(request.PaymentId);
                    }

                }

                return json;
            }
            catch (Exception ex)
            {
                JsonResult json = new JsonResult(new { status = "error", message = ex.Message });
                return json;
            }

        }

        [HttpPost]
        [Route("0/CancelPaymentStripe")]
        public JsonResult CancelPaymentStripe([FromBody] CancelPaymentStripeRequest request)
        {
            try
            {
                var paymentIntents = new PaymentIntentService();
                var paymentCancel = paymentIntents.Cancel(request.PaymentId);

                JsonResult json = new JsonResult(new { status = "OK", message = paymentCancel.Status });

                if (paymentCancel.StripeResponse.StatusCode.Equals("OK"))
                {
                    int resul = BDonorStripe.Cancel(request.PaymentId);
                }

                return json;
            }
            catch (Exception ex)
            {
                JsonResult json = new JsonResult(new { status = "error", message = ex.Message });
                return json;
            }

        }

        [HttpPost]
        [Route("0/CreatePaymentStripe")]
        public JsonResult CreatePaymentStripe([FromBody] PaymentStripeRequest request)
        {
            try
            {
                var paymentIntents = new PaymentIntentService();
                var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
                {
                    Amount = request.Amount * 100,
                    Currency = "usd",
                    Description = "Unitlife Donation",
                });

                JsonResult json = new JsonResult(new { clientSecret = paymentIntent.ClientSecret, PaymentId = paymentIntent.Id, status = "ok", message = "success" });

                int resul = BDonorStripe.Create(paymentIntent.Id, paymentIntent.ClientSecret, request.DonorId);

                return json;
            }
            catch (Exception ex)
            {
                JsonResult json = new JsonResult(new { status = "error", message = ex.Message });
                return json;
            }
        }

        // POST api/values
        [HttpPost]
        [Route("0/SaveDonation")]
        public DonationResponse SaveDonation([FromBody] DonationRequest request)
        {
            DonationResponse response = new DonationResponse();
            MDonation donation = new MDonation();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            try
            {
                BaseRequest baseRequest = new BaseRequest();

                donation.DonorId = request.Donation.DonorId;
                donation.Date = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd"));
                donation.Amount = request.Donation.Amount;
                donation.PaymentType = request.Donation.PaymentType;
                donation.ProjectId = request.Donation.ProjectId;

                MDonorFrequency donorFrequencyBE = new MDonorFrequency();
                donorFrequencyBE = request.DonorFrequency;

                MPayMethod payMethodBE = new MPayMethod();

                if (request.PayMethod == null)
                {
                    payMethodBE.DonorStripe = new MDonorStripe();
                }
                else
                {
                    payMethodBE.DonorStripe = request.PayMethod.DonorStripe;
                }

                baseRequest.Language = request.Language;
                baseRequest.Session = request.Session;

                BaseResponse baseResponse = new BaseResponse();
                
                if (donation.PaymentType.Equals("4"))
                {
                    baseResponse = BDonation.Insert(donation, donorFrequencyBE, payMethodBE, baseRequest);
                }

                if (baseResponse.Code.Equals("0"))
                {
                    string webRoot = _env.ContentRootPath;
                    string CertifcateName = BDonation.GenarteCerticate(donation.DonorId, webRoot, string.Format("{0:0.##}", donation.Amount), _MAwsS3);

                    donation.Certificate = CertifcateName.Replace("[WSTAMP]", ""); ;

                    BDonation.Update(donation, baseRequest);
                }

                response.Code = baseResponse.Code;
                response.Message = baseResponse.Message;
                response.Donation = donation;
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
                response.Donation = donation;
            }

            return response;
        }

        [HttpPost]
        [Route("0/GetDonations")]
        public ProjectDonationsResponse GetDonations([FromBody] DonationRequest request)
        {
            ProjectDonationsResponse response = new ProjectDonationsResponse();
            List<MProjectDonation> donations = new List<MProjectDonation>();
            MDonor donor = new MDonor();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            donor.DonorId = request.Donation.DonorId;

            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;


            donations = BProjectDonation.List(donor, baseRequest);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.Donations = donations.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetTotals")]
        public DonationTotalResponse GetTotals([FromBody] DonationRequest request)
        {
            DonationTotalResponse response = new DonationTotalResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            List<decimal> totales = new List<decimal>();

            totales = BDonation.GetTotals();

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.FoodsTotal = totales[1];
            response.DonationTotal = totales[0];
            //response.FoodsTotal = 155000;
            //response.DonationTotal = 235878561;

            return response;
        }

        [HttpPost]
        [Route("0/SendCertificate")]
        public DonationResponse SendCertificate([FromBody] CertificateRequest request)
        {
            DonationResponse response = new DonationResponse();
            MDonation donation = new MDonation();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            donation.DonationId = request.DonationId;

            int Val = 0;

            donation = BDonation.Select(donation, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                try
                {
                    int val = 0;
                    MParameter parameterBE = new MParameter();
                    List<MParameter> parameterBEs = new List<MParameter>();

                    parameterBE.Code = "MAIL_CERT";
                    parameterBEs = BParameter.List(parameterBE, ref val);

                    if (val.Equals(0))
                    {
                        string Message = parameterBEs[0].Valor1.Replace("[CERTIFICATE]", Path.Combine(Constant.S3Server, "certificates") + "/" + donation.Certificate);
                        string Subject = "UNITLIFE - Donation certificate";

                        if (request.Email.Equals(""))
                        {
                            request.Email = donation.Email;
                        }

                        SendSES(Subject, Message, request.Email);
                    }

                }
                catch (Exception ex)
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = "Failed to send the certificate.";
                }

            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = "Failed to send the certificate.";
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = "Donation does not exist.";
            }

            response.Donation = donation;


            return response;
        }

        private void SendSES(string Subject, string Message, string Email)
        {
            _MAwsEmail.Subject = Subject;
            _MAwsEmail.Message = Message;
            _MAwsEmail.ToEmail = Email;

            BAwsSDK.SendEmailAsync(_MAwsEmail);
        }
    }
}
