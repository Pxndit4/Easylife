using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiIntroduction
    {
        public List<MIntroduction> GetIntroductions(MIntroduction MIntroduction, Session Session)
        {
            List<MIntroduction> banners = new List<MIntroduction>();
            IntroductionRequest request = new IntroductionRequest();
            IntroductionsResponse response = new IntroductionsResponse();

            MIntroduction.Title = MIntroduction.Title;
            request.Session = Session;
            MIntroduction.Status = MIntroduction.Status;

            request.Language = "ENG";
            request.Introduction = MIntroduction;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Introduction", "FilterIntroductions", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    banners = response.Introductions;
                }
            }

            return banners;
        }

        public MIntroduction GetIntroduction(MIntroduction MIntroduction, Session Session)
        {
            MIntroduction banner = new MIntroduction();
            IntroductionRequest request = new IntroductionRequest();
            IntroductionResponse response = new IntroductionResponse();

            request.Introduction = MIntroduction;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Introduction", "GetIntroduction", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    banner = response.Introduction;
                }
            }

            return banner;
        }

        public string InsertIntroduction(MIntroduction MIntroduction, Session Session)
        {
            IntroductionRequest request = new IntroductionRequest();
            IntroductionResponse response = new IntroductionResponse();
            string returnMsg = string.Empty;

            request.Introduction = MIntroduction;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Introduction", "InsertIntroduction", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Introduction api";
            }

            return returnMsg;
        }

        public string UpdateIntroduction(MIntroduction MIntroduction, Session Session)
        {
            IntroductionRequest request = new IntroductionRequest();
            IntroductionResponse response = new IntroductionResponse();
            string returnMsg = string.Empty;

            request.Introduction = MIntroduction;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Introduction", "UpdateIntroduction", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Introduction api";
            }

            return returnMsg;
        }

        public string DeleteIntroduction(MIntroduction MIntroduction, Session Session)
        {
            IntroductionRequest request = new IntroductionRequest();
            IntroductionResponse response = new IntroductionResponse();
            string returnMsg = string.Empty;

            request.Introduction = MIntroduction;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Introduction", "DeleteIntroduction", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Introduction api";
            }

            return returnMsg;
        }
    }

    internal class IntroductionRequest
    {
        public MIntroduction Introduction { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }

        public string Language { get; set; }
    }

    internal class IntroductionsResponse
    {
        public List<MIntroduction> Introductions { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class IntroductionResponse
    {
        public MIntroduction Introduction { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
