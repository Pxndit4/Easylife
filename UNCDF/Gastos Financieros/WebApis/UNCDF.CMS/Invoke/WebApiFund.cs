using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiFund
    {
        public List<MFund> GetFunds()
        {
            List<MFund> funds = new List<MFund>();
            BaseRequest request = new BaseRequest();
            FundsResponse response = new FundsResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Fund", "GetFunds", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<FundsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    funds = response.Funds;
                }
            }

            return funds;
        }
    }

    public class FundsResponse : BaseResponse
    {
        public List<MFund> Funds { get; set; }
    }

    public class FundsRequest : BaseRequest
    {
        public List<MFund> Funds { get; set; }
    }
}