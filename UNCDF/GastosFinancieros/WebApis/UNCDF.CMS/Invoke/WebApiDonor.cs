using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiDonor
    {
        public List<MDonor> GetDonors(MDonor MDonor, Session eSession)
        {
            List<MDonor> donors = new List<MDonor>();
            DonorRequest request = new DonorRequest();
            DonorsResponse response = new DonorsResponse();

            request.Donor = MDonor;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("donation/api/Donor", "GetDonors", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DonorsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    donors = response.Donors;
                }
            }

            return donors;
        }

    }


    [Serializable]
    public class DonorRequest : BaseRequest
    {
        public MDonor Donor { get; set; }
    }



    [Serializable]
    public class DonorsResponse : BaseResponse
    {
        public List<MDonor> Donors { get; set; }
     
    }

}