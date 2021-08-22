using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiParameter
    {
        public List<MParameter> GetParameters(MParameter MParameter, Session Session)
        {
            List<MParameter> parameters = new List<MParameter>();
            ParameterRequest request = new ParameterRequest();
            ParametersResponse response = new ParametersResponse();

            MParameter.Description = MParameter.Description;
            MParameter.Status = MParameter.Status;

            request.Parameter = MParameter;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Parameter", "GetParameters", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ParametersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    parameters = response.Parameters;
                }
            }

            return parameters;
        }

        public MParameter GetParameter(MParameter MParameter, Session Session)
        {
            MParameter parameter = new MParameter();
            ParameterRequest request = new ParameterRequest();
            ParameterResponse response = new ParameterResponse();

            request.Parameter = MParameter;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Parameter", "GetParameter", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ParameterResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    parameter = response.Parameter;
                }
            }

            return parameter;
        }


        public string UpdateParameter(MParameter MParameter, Session Session)
        {
            ParameterRequest request = new ParameterRequest();
            ParameterResponse response = new ParameterResponse();
            string returnMsg = string.Empty;

            request.Parameter = MParameter;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Parameter", "UpdateParameter", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ParameterResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Parameter api";
            }

            return returnMsg;
        }

    }

    internal class ParameterRequest
    {
        public MParameter Parameter { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class ParametersResponse
    {
        public List<MParameter> Parameters { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class ParameterResponse
    {
        public MParameter Parameter { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}