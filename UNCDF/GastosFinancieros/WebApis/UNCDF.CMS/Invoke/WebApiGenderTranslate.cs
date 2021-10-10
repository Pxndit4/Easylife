using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiGenderTranslate
    {
        public List<MGenderTranslate> GetGenderTranslates(MGenderTranslate eGender, Session Session)
        {

            GenderTranslatesResponse response = new GenderTranslatesResponse();
            GenderTranslateRequest request = new GenderTranslateRequest();
            List<MGenderTranslate> genders = new List<MGenderTranslate>();

            //eGender.GenderId = eGender.GenderId;

            request.GenderTranslateBE = eGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/GenderTranslate", "GetGenderTranslates", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderTranslatesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genders = response.GenderTranslates;
                }
            }

            return genders;
        }

        public MGenderTranslate GetGenderTranslate(MGenderTranslate MGenderTranslate, Session Session)
        {
            MGenderTranslate genderE = new MGenderTranslate();
            GenderTranslateRequest request = new GenderTranslateRequest();
            GenderTranslateResponse response = new GenderTranslateResponse();

            request.GenderTranslateBE = MGenderTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/GenderTranslate", "GetGenderTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderTranslateResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genderE = response.GenderTranslate;
                }
            }

            return genderE;
        }

        public string InsertGenderTranslate(MGenderTranslate MGenderTranslate, Session Session)
        {
            GenderTranslateRequest request = new GenderTranslateRequest();
            GenderTranslatesResponse response = new GenderTranslatesResponse();
            string returnMsg = string.Empty;

            request.GenderTranslateBE = MGenderTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/GenderTranslate", "InsertGenderTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }

        public string UpdateGenderTranslate(MGenderTranslate MGenderTranslate, Session Session)
        {
            GenderTranslateRequest request = new GenderTranslateRequest();
            GenderTranslateResponse response = new GenderTranslateResponse();
            string returnMsg = string.Empty;

            request.GenderTranslateBE = MGenderTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/GenderTranslate", "UpdateGenderTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderTranslateResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }

        public string DeleteGender(MGenderTranslate MGenderTranslate, Session Session)
        {
            GenderTranslateRequest request = new GenderTranslateRequest();
            GenderTranslatesResponse response = new GenderTranslatesResponse();
            string returnMsg = string.Empty;

            request.GenderTranslateBE = MGenderTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/GenderTranslate", "DeleteGenderTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }
    }

    internal class GenderTranslateRequest
    {
        public MGenderTranslate GenderTranslateBE { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class GenderTranslatesResponse
    {
        public List<MGenderTranslate> GenderTranslates { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class GenderTranslateResponse
    {
        public MGenderTranslate GenderTranslate { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
