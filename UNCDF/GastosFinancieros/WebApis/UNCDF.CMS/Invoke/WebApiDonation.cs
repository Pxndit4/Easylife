using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiDonation
    {
        public List<MDonation> ListDonation(MDonation MDonation, Session eSession)
        {
            List<MDonation> donations = new List<MDonation>();
            DonationRequest request = new DonationRequest();
            DonationsResponse response = new DonationsResponse();

            request.Donation = MDonation;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("donation/api/Donation", "DonationList", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DonationsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    donations = response.Donations;
                }
            }

            return donations;
        }

    }
    internal class DonationRequest
    {
        public MDonation Donation { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class DonationsResponse
    {
        public List<MDonation> Donations { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}

