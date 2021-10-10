using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PayPal.Api;
using Stripe;
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

namespace UNCDF.WebApi.Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class DonationController : Controller
    {

        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;
        private readonly MPaypal _myPaypalSettings;
        private readonly IWebHostEnvironment _env;

        public DonationController(IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3, IOptions<MPaypal> MPaypal,
                                    IWebHostEnvironment env)
        {
            _env = env;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
            _myPaypalSettings = MPaypal.Value;
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

        [HttpPost]
        [Route("0/PaypalConfig")]
        public PaypalConfigResponse PaypalConfig([FromBody] BaseRequest request)
        {
            PaypalConfigResponse response = new PaypalConfigResponse();
            response.PaypalConfig = new MPaypal();
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
                
                int Val = 0;

                List<MParameter> lst = BParameter.List(new MParameter { Code = "PAYPAL_CLIENTID", Description = "" }, ref Val);

                response.PaypalConfig.clientId = lst[0].Valor1.ToString();

                lst = BParameter.List(new MParameter { Code = "PAYPAL_MODE", Description = "" }, ref Val);
                response.PaypalConfig.mode = lst[0].Valor1.ToString();

                lst = BParameter.List(new MParameter { Code = "PAYPAL_SECRECT", Description = "" }, ref Val);
                response.PaypalConfig.clientSecret = lst[0].Valor1.ToString();

                response.Code = "0";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = Messages.ErrorPayment;
            }

            return response;
        }

        [HttpPost]
        [Route("0/PaypalDonation")]
        public DonationPaypalResponse PaypalDonation([FromBody] DonationPaypalRequest request)
        {
            DonationPaypalResponse response = new DonationPaypalResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            var accessToken = new OAuthTokenCredential(_myPaypalSettings.clientId, _myPaypalSettings.clientSecret).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            string baseURIOk = string.Empty;
            string baseURIFail = string.Empty;

            baseURIOk = "http://3.23.158.238/uncdf/donations/periodically/thank-you?";

            baseURIFail = "http://3.23.158.238/uncdf/donations/error?";

            //baseURIOk = "http://localhost:4200/uncdf/donations/periodically/thank-you?";

            //baseURIFail = "http://localhost:4200/uncdf/donations/error?";

            try
            {
                Payment payment = null;

                Item item = new Item();
                item.name = "Donation UNCDF";
                item.currency = "USD";
                item.price = request.Amount.ToString();
                item.quantity = "1";
                item.sku = "donation";
                item.description = "Your donation will help feed families around the world.";

                List<Item> itms = new List<Item>();
                itms.Add(item);
                ItemList itemList = new ItemList();
                itemList.items = itms;

                Payer payr = new Payer();
                payr.payment_method = "paypal";
                Random rndm = new Random();
                var guid = Convert.ToString(rndm.Next(100000));

                RedirectUrls redirUrls = new RedirectUrls();
                redirUrls.cancel_url = baseURIFail + "guid=" + guid;
                redirUrls.return_url = baseURIOk + "guid=" + guid;

                Details details = new Details();
                details.tax = "0";
                details.shipping = "0";
                details.subtotal = request.Amount.ToString();

                Amount amnt = new Amount();
                amnt.currency = "USD";
                amnt.total = request.Amount.ToString();
                amnt.details = details;

                List<Transaction> transactionList = new List<Transaction>();
                Transaction tran = new Transaction();
                tran.description = "Donation UNCDF";
                tran.amount = amnt;
                tran.item_list = itemList;

                transactionList.Add(tran);

                payment = new Payment();
                payment.intent = "sale";
                payment.payer = payr;
                payment.transactions = transactionList;
                payment.redirect_urls = redirUrls;


                var createdPayment = payment.Create(apiContext);
                string paypalRedirectUrl = null;
                var links = createdPayment.links.GetEnumerator();

                while (links.MoveNext())
                {
                    Links lnk = links.Current;

                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment
                        paypalRedirectUrl = lnk.href;
                    }
                }

                response.Code = "0";
                response.PaymentId = createdPayment.id;
                response.Message = Messages.Success;
                response.PaymentURL = paypalRedirectUrl;
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = Messages.ErrorPayment;
                response.PaymentURL = baseURIFail;
            }

            return response;
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
                    payMethodBE.DonorPayPal = new MDonorPayPal();
                }
                else
                {
                    payMethodBE.DonorStripe = request.PayMethod.DonorStripe;
                    payMethodBE.DonorPayPal = request.PayMethod.DonorPayPal;
                }

                StringBuilder strBodyFrecuency = new StringBuilder();
                DateTime dtNow = DateTime.Today;
                

                if (request.DonorFrequency != null)
                {
                    strBodyFrecuency.AppendLine("Dear donor, this is the schedule for your next donations:");
                    
                    strBodyFrecuency.AppendLine("<Table style='padding-top: 12px; width: 40%; border: 1px solid #000; padding-bottom: 12px; text-align: left; background-color: #04AA6D; color: white;'>");

                    strBodyFrecuency.AppendLine("<tr>");
                    strBodyFrecuency.AppendLine("<th>");
                    strBodyFrecuency.AppendLine("Nro.");
                    strBodyFrecuency.AppendLine("</th>");
                    strBodyFrecuency.AppendLine("<th>");
                    strBodyFrecuency.AppendLine("Fecha");
                    strBodyFrecuency.AppendLine("</th>");
                    strBodyFrecuency.AppendLine("</tr>");

                    for (int i = 1; i <= request.DonorFrequency.Quantity; i++)
                    {
                        dtNow = dtNow.AddMonths(request.DonorFrequency.Frequency);

                        strBodyFrecuency.AppendLine("<tr>");
                        strBodyFrecuency.AppendLine("<td style='background-color: #ffffff; color: black;'>");
                        strBodyFrecuency.AppendLine(i.ToString());
                        strBodyFrecuency.AppendLine("</td>");
                        strBodyFrecuency.AppendLine("<td style='background-color: #ffffff; color: black;'>");
                        strBodyFrecuency.AppendLine(dtNow.ToShortDateString());
                        strBodyFrecuency.AppendLine("</td>");
                        strBodyFrecuency.AppendLine("</tr>");
                    }

                    strBodyFrecuency.AppendLine("</Table>");
                }

                baseRequest.Language = request.Language;
                baseRequest.Session = request.Session;

                BaseResponse baseResponse = new BaseResponse();

                if (donation.PaymentType.Equals("4"))
                {
                    baseResponse = BDonation.Insert(donation, donorFrequencyBE, payMethodBE, baseRequest);
                }
                else if (donation.PaymentType.Equals("2"))
                {
                    if (payMethodBE.DonorPayPal.Guid != null)
                    {
                        var accessToken = new OAuthTokenCredential(_myPaypalSettings.clientId, _myPaypalSettings.clientSecret).GetAccessToken();
                        var apiContext = new APIContext(accessToken);

                        var guid = payMethodBE.DonorPayPal.Guid;
                        var paymentId = payMethodBE.DonorPayPal.PaymentId;
                        var paymentExecution = new PaymentExecution() { payer_id = payMethodBE.DonorPayPal.PayerID };
                        var pymnt = new Payment() { id = paymentId };
                        var executedPayment = pymnt.Execute(apiContext, paymentExecution);
                    }

                    baseResponse = BDonation.Insert(donation, donorFrequencyBE, payMethodBE, baseRequest);
                }

                if (baseResponse.Code.Equals("0"))
                {
                    string webRoot = _env.ContentRootPath;
                    string CertifcateName = BDonation.GenarteCerticate(donation.DonorId, webRoot, string.Format("{0:0.##}", donation.Amount), _MAwsS3);

                    donation.Certificate = CertifcateName.Replace("[WSTAMP]", ""); ;

                    BDonation.Update(donation, baseRequest);
                }

                if (request.DonorFrequency != null)
                {
                    int val = 0;

                    donation = BDonation.Select(donation, ref val);
                                        
                    if (val.Equals(0))
                    {
                        string Message = strBodyFrecuency.ToString();
                        string Subject = "UNCDF - Donation Frequency";                        

                        SendSES(Subject, Message, donation.Email);
                    }
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
                        string Subject = "UNCDF - Donation certificate";

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
