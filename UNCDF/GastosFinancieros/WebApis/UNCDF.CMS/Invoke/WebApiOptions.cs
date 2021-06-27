using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiOptions
    {
        public List<MOptions> GetOptions(MOptions MOptions, Session eSession)
        {
            List<MOptions> options = new List<MOptions>();
            OptionsRequest request = new OptionsRequest();
            OptionsResponse response = new OptionsResponse();

            request.Options = MOptions;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Options", "GetOptions", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<OptionsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    options = response.Options;
                }
            }

            return options;
        }

        public List<MOptions> GetOptionsByProfile(MOptions MOptions, Session eSession)
        {
            List<MOptions> options = new List<MOptions>();
            OptionsRequest request = new OptionsRequest();
            OptionsResponse response = new OptionsResponse();

            request.Options = MOptions;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("security/api/Options", "GetOptionsByProfile", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<OptionsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    options = response.Options;
                }
            }

            return options;
        }
    }

    public class OptionsRequest
    {
        public MOptions Options { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    public class OptionsResponse
    {
        public List<MOptions> Options { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}