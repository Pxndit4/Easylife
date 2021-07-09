using Microsoft.AspNetCore.Cors;
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
    }
}
