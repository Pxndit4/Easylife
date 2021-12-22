using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiDonorPartner
    {
        public List<MDonorPartner> GetDonorPartners()
        {
            List<MDonorPartner> funds = new List<MDonorPartner>();
            BaseRequest request = new BaseRequest();
            DonorPartnersResponse response = new DonorPartnersResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/DonorPartner", "GetDonorPartners", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DonorPartnersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    funds = response.DonorPartners;
                }
            }

            return funds;
        }

        public string InsertDonorPartner(List<MDonorPartner> list,int TotalCorrect, int  TotalBad,  Session eSession)
        {
            DonorPartnersRequest request = new DonorPartnersRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.DonorPartners = list;
            
            request.TotalCorrect = TotalCorrect;
            request.TotalBad = TotalBad;

            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/DonorPartner", "InsertDonorPartner", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }
    }

    public class DonorPartnersResponse : BaseResponse
    {
        public List<MDonorPartner> DonorPartners { get; set; }
    }

    public class DonorPartnersRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public List<MDonorPartner> DonorPartners { get; set; }

    }

}