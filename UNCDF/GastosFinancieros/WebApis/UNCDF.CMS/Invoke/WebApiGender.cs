using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiGender
    {
        public List<MGender> GetGenders(MGender MGender, Session Session)
        {
            List<MGender> genders = new List<MGender>();
            GenderRequest request = new GenderRequest();
            GendersResponse response = new GendersResponse();

            MGender.Description = MGender.Description;
            MGender.Status = MGender.Status;

            request.GenderBE = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Gender", "GetGenders", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GendersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genders = response.Genders;
                }
            }

            return genders;
        }

        public MGender GetGender(MGender MGender, Session Session)
        {
            MGender gender = new MGender();
            GenderRequest request = new GenderRequest();
            GenderResponse response = new GenderResponse();

            request.GenderBE = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Gender", "GetGender", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    gender = response.Gender;
                }
            }

            return gender;
        }

        public string InsertGender(MGender MGender, Session Session)
        {
            GenderRequest request = new GenderRequest();
            GenderResponse response = new GenderResponse();
            string returnMsg = string.Empty;

            request.GenderBE = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Gender", "InsertGender", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }

        public string UpdateGender(MGender MGender, Session Session)
        {
            GenderRequest request = new GenderRequest();
            GenderResponse response = new GenderResponse();
            string returnMsg = string.Empty;

            request.GenderBE = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Gender", "UpdateGender", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }

        public string DeleteGender(MGender MGender, Session Session)
        {
            GenderRequest request = new GenderRequest();
            GenderResponse response = new GenderResponse();
            string returnMsg = string.Empty;

            request.GenderBE = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Gender", "DeleteGender", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<GenderResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }
    }

    internal class GenderRequest
    {
        public MGender GenderBE { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class GendersResponse
    {
        public List<MGender> Genders { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class GenderResponse
    {
        public MGender Gender { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}