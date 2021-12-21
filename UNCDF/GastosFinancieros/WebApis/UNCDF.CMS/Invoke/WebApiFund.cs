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
            string bodyresponse = new Helper().InvokeApi("Project/api/Fund", "GetFunds", bodyrequest, ref statuscode);

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

        public string InsertFund(List<MFund> list,  int TotalCorrect, int TotalBad, Session eSession)
        {
            FundsRequest request = new FundsRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.Funds = list;
            request.Session = eSession;
            request.TotalCorrect = TotalCorrect;
            request.TotalBad = TotalBad;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("project/api/Fund", "InsertFund", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }
    }

    public class FundsResponse : BaseResponse
    {
        public List<MFund> Funds { get; set; }
    }

    public class FundsRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public List<MFund> Funds { get; set; }
    }
}