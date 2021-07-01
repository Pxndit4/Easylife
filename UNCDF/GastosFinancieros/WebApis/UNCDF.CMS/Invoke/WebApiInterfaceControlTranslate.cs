using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNCDF.Layers.Model;
using System.Configuration;
using Newtonsoft.Json;

namespace UNCDF.CMS
{
    public class WebApiInterfaceControlTranslate
    {
        public List<MInterfaceControlTranslate> GetInterfaceControlTranslates(MInterfaceControlTranslate eInterfaceControl, Session eSession)
        {

            InterfaceControlTranslatesResponse response = new InterfaceControlTranslatesResponse();
            InterfaceControlTranslateRequest request = new InterfaceControlTranslateRequest();
            List<MInterfaceControlTranslate> interfaces = new List<MInterfaceControlTranslate>();

            eInterfaceControl.InterfaceControlId = eInterfaceControl.InterfaceControlId;

            request.MInterfaceControlTranslate = eInterfaceControl;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControlTranslate", "GetInterfaceControlTranslates", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlTranslatesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    interfaces = response.InterfaceControlTranslates;
                }
            }

            return interfaces;
        }

        public MInterfaceControlTranslate GetInterfaceControlTranslate(MInterfaceControlTranslate MInterfaceControlTranslate, Session eSession)
        {
            MInterfaceControlTranslate interfaceE = new MInterfaceControlTranslate();
            InterfaceControlTranslateRequest request = new InterfaceControlTranslateRequest();
            InterfaceControlTranslateResponse response = new InterfaceControlTranslateResponse();

            request.MInterfaceControlTranslate = MInterfaceControlTranslate;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControlTranslate", "GetInterfaceControlTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlTranslateResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    interfaceE = response.InterfaceControlTranslate;
                }
            }

            return interfaceE;
        }

        public string InsertInterfaceControlTranslate(MInterfaceControlTranslate MInterfaceControlTranslate, Session eSession)
        {
            InterfaceControlTranslateRequest request = new InterfaceControlTranslateRequest();
            InterfaceControlTranslatesResponse response = new InterfaceControlTranslatesResponse();
            string returnMsg = string.Empty;

            request.MInterfaceControlTranslate = MInterfaceControlTranslate;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControlTranslate", "InsertInterfaceControlTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking InterfaceControl api";
            }

            return returnMsg;
        }

        public string UpdateInterfaceControlTranslate(MInterfaceControlTranslate MInterfaceControlTranslate, Session eSession)
        {
            InterfaceControlTranslateRequest request = new InterfaceControlTranslateRequest();
            InterfaceControlTranslateResponse response = new InterfaceControlTranslateResponse();
            string returnMsg = string.Empty;

            request.MInterfaceControlTranslate = MInterfaceControlTranslate;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControlTranslate", "UpdateInterfaceControlTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlTranslateResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking InterfaceControl api";
            }

            return returnMsg;
        }

        public string DeleteInterfaceControl(MInterfaceControlTranslate MInterfaceControlTranslate, Session eSession)
        {
            InterfaceControlTranslateRequest request = new InterfaceControlTranslateRequest();
            InterfaceControlTranslatesResponse response = new InterfaceControlTranslatesResponse();
            string returnMsg = string.Empty;

            request.MInterfaceControlTranslate = MInterfaceControlTranslate;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControlTranslate", "DeleteInterfaceControlTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking InterfaceControl api";
            }

            return returnMsg;
        }
    }

    internal class InterfaceControlTranslateRequest
    {
        public MInterfaceControlTranslate MInterfaceControlTranslate { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class InterfaceControlTranslatesResponse
    {
        public List<MInterfaceControlTranslate> InterfaceControlTranslates { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class InterfaceControlTranslateResponse
    {
        public MInterfaceControlTranslate InterfaceControlTranslate { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
