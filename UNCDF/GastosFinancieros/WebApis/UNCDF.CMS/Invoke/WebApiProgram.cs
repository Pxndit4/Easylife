using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiProgram
    {
        public List<MProgramName> GetProgramNames()
        {
            List<MProgramName> funds = new List<MProgramName>();
            BaseRequest request = new BaseRequest();
            ProgramNamesResponse response = new ProgramNamesResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProgramName", "GetProgramNames", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProgramNamesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    funds = response.ProgramNames;
                }
            }

            return funds;
        }
        public List<MProgramName> GetValidProgramNames()
        {
            List<MProgramName> funds = new List<MProgramName>();
            BaseRequest request = new BaseRequest();
            ProgramNamesResponse response = new ProgramNamesResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProgramName", "GetValidProgramNames", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProgramNamesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    funds = response.ProgramNames;
                }
            }

            return funds;
        }
        public string InsertProgramName(List<MProgramName> list, int TotalCorrect, int TotalBad, Session eSession)
        {
            ProgramNamesRequest request = new ProgramNamesRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.ProgramNames = list;

            request.TotalCorrect = TotalCorrect;
            request.TotalBad = TotalBad;

            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("project/api/ProgramName", "InsertProgramName", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }
    }

    public class ProgramNamesResponse : BaseResponse
    {
        public List<MProgramName> ProgramNames { get; set; }
    }

    [Serializable]
    public class ProgramNamesRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public List<MProgramName> ProgramNames { get; set; }
    }
}