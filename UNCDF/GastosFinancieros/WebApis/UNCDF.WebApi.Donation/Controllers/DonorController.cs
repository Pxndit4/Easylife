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

namespace UNCDF.WebApi.Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class DonorController : Controller
    {
        // GET: DonorController
        [HttpPost]
        [Route("0/GetDonors")]
        public DonorResponse GetDonors([FromBody] DonorRequest request)
        {
            DonorResponse response = new DonorResponse();
            List<MDonor> list = new List<MDonor>();
            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MDonor ent = new MDonor();

            ent.FirstName = request.Donor.FirstName;
            ent.LastName = request.Donor.LastName;
            ent.CountryId = request.Donor.CountryId;
            ent.Registered = request.Donor.Registered;
            ent.Status = request.Donor.Status;

            int Val = 0;

            list = BDonor.List(ent, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Donors");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Donors");
            }

            response.Donors = list.ToArray();

            return response;
        }
    }
}
