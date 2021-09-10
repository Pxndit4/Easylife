using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiIntroductionTranslate
    {
        public List<MIntroductionTranslate> GetIntroductionTranslates(MIntroductionTranslate eIntroduction, Session Session)
        {

            IntroductionTranslatesResponse response = new IntroductionTranslatesResponse();
            IntroductionTranslateRequest request = new IntroductionTranslateRequest();
            List<MIntroductionTranslate> genders = new List<MIntroductionTranslate>();

            //eIntroduction.IntroductionId = eIntroduction.IntroductionId;

            request.IntroductionTranslateBE = eIntroduction;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/IntroductionTranslate", "GetIntroductionTranslates", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionTranslatesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genders = response.IntroductionTranslates;
                }
            }

            return genders;
        }

        public MIntroductionTranslate GetIntroductionTranslate(MIntroductionTranslate MIntroductionTranslate, Session Session)
        {
            MIntroductionTranslate genderE = new MIntroductionTranslate();
            IntroductionTranslateRequest request = new IntroductionTranslateRequest();
            IntroductionTranslateResponse response = new IntroductionTranslateResponse();

            request.IntroductionTranslateBE = MIntroductionTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/IntroductionTranslate", "GetIntroductionTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionTranslateResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genderE = response.IntroductionTranslate;
                }
            }

            return genderE;
        }

        public string InsertIntroductionTranslate(MIntroductionTranslate MIntroductionTranslate, Session Session)
        {
            IntroductionTranslateRequest request = new IntroductionTranslateRequest();
            IntroductionTranslatesResponse response = new IntroductionTranslatesResponse();
            string returnMsg = string.Empty;

            request.IntroductionTranslateBE = MIntroductionTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/IntroductionTranslate", "InsertIntroductionTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Introduction api";
            }

            return returnMsg;
        }

        public string UpdateIntroductionTranslate(MIntroductionTranslate MIntroductionTranslate, Session Session)
        {
            IntroductionTranslateRequest request = new IntroductionTranslateRequest();
            IntroductionTranslateResponse response = new IntroductionTranslateResponse();
            string returnMsg = string.Empty;

            request.IntroductionTranslateBE = MIntroductionTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/IntroductionTranslate", "UpdateIntroductionTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionTranslateResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Introduction api";
            }

            return returnMsg;
        }

        public string DeleteIntroduction(MIntroductionTranslate MIntroductionTranslate, Session Session)
        {
            IntroductionTranslateRequest request = new IntroductionTranslateRequest();
            IntroductionTranslatesResponse response = new IntroductionTranslatesResponse();
            string returnMsg = string.Empty;

            request.IntroductionTranslateBE = MIntroductionTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/IntroductionTranslate", "DeletMIntroductionTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<IntroductionTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Introduction api";
            }

            return returnMsg;
        }
    }

    internal class IntroductionTranslateRequest
    {
        public MIntroductionTranslate IntroductionTranslateBE { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class IntroductionTranslatesResponse
    {
        public List<MIntroductionTranslate> IntroductionTranslates { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class IntroductionTranslateResponse
    {
        public MIntroductionTranslate IntroductionTranslate { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
