using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiLanguage
    {
        public List<MLanguage> GetLanguages(MLanguage eLanguage, Session eSession)
        {
            List<MLanguage> languages = new List<MLanguage>();
            LanguageRequest request = new LanguageRequest();
            LanguagesResponse response = new LanguagesResponse();

            eLanguage.Description = eLanguage.Description;
            eLanguage.Status = eLanguage.Status;

            request.MLanguage = eLanguage;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "FilterLanguages", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<LanguagesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    languages = response.Languages;
                }
            }

            return languages;
        }

        public MLanguage GetLanguage(MLanguage ELanguage, Session eSession)
        {
            MLanguage language = new MLanguage();
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();

            request.MLanguage = ELanguage;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "GetLanguage", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<LanguageResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    language = response.Language;
                }
            }

            return language;
        }

        public string InsertLanguage(MLanguage ELanguage, Session eSession)
        {
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();
            string returnMsg = string.Empty;

            request.MLanguage = ELanguage;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "InsertLanguage", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<LanguageResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Language api";
            }

            return returnMsg;
        }

        public string UpdateLanguage(MLanguage ELanguage, Session eSession)
        {
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();
            string returnMsg = string.Empty;

            request.MLanguage = ELanguage;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "UpdateLanguage", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<LanguageResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Language api";
            }

            return returnMsg;
        }

        public string DeleteLanguage(MLanguage ELanguage, Session eSession)
        {
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();
            string returnMsg = string.Empty;

            request.MLanguage = ELanguage;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "DeleteLanguage", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<LanguageResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Language api";
            }

            return returnMsg;
        }
    }

    internal class LanguageRequest
    {
        public MLanguage MLanguage { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class LanguagesResponse
    {
        public List<MLanguage> Languages { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class LanguageResponse
    {
        public MLanguage Language { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}