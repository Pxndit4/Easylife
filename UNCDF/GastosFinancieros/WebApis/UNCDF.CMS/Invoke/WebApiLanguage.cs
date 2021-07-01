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
        public List<MLanguage> GetLanguages(MLanguage MLanguage, Session Session)
        {
            List<MLanguage> languages = new List<MLanguage>();
            LanguageRequest request = new LanguageRequest();
            LanguagesResponse response = new LanguagesResponse();

            MLanguage.Description = MLanguage.Description;
            MLanguage.Status = MLanguage.Status;

            request.LanguageBE = MLanguage;
            request.Session = Session;
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

        public MLanguage GetLanguage(MLanguage MLanguage, Session Session)
        {
            MLanguage language = new MLanguage();
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();

            request.LanguageBE = MLanguage;
            request.Session = Session;
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

        public string InsertLanguage(MLanguage MLanguage, Session Session)
        {
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();
            string returnMsg = string.Empty;

            request.LanguageBE = MLanguage;
            request.Session = Session;
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

        public string UpdateLanguage(MLanguage MLanguage, Session Session)
        {
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();
            string returnMsg = string.Empty;

            request.LanguageBE = MLanguage;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "UpdatMLanguage", bodyrequest, ref statuscode);

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

        public string DeleteLanguage(MLanguage MLanguage, Session Session)
        {
            LanguageRequest request = new LanguageRequest();
            LanguageResponse response = new LanguageResponse();
            string returnMsg = string.Empty;

            request.LanguageBE = MLanguage;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Language", "DeletMLanguage", bodyrequest, ref statuscode);

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
        public MLanguage LanguageBE { get; set; }
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
