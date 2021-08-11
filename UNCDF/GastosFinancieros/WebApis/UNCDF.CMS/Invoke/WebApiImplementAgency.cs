using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS
{
    public class WebApiImplementAgency
    {
        public List<MImplementAgency> GetImplementAgencys()
        {
            List<MImplementAgency> ImplementAgencys = new List<MImplementAgency>();
            BaseRequest request = new BaseRequest();
            ImplementAgencysResponse response = new ImplementAgencysResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ImplementAgency", "GetImplementAgencies", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ImplementAgencysResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    ImplementAgencys = response.ImplementAgencies;
                }
            }

            return ImplementAgencys;
        }

        public string InsertImplementAgency(List<MImplementAgency> list, Session eSession)
        {
            ImplementAgencysRequest request = new ImplementAgencysRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.ImplementAgencies = list;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ImplementAgency", "InsertImplementAgency", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }
    }

    public class ImplementAgencysResponse : BaseResponse
    {
        public List<MImplementAgency> ImplementAgencies { get; set; }
    }

    public class ImplementAgencysRequest : BaseRequest
    {
        public List<MImplementAgency> ImplementAgencies { get; set; }
    }
}