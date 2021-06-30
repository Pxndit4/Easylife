using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNCDF.Layers.Model;
using System.Configuration;
using Newtonsoft.Json;

namespace UNCDF.CMS.Invoke
{
    public class WebApiInterfaceControl
    {
        public List<MInterfaceControl> GetInterfaceControls(MInterfaceControl eInterfaceControl, Session eSession)
        {

            InterfaceControlsResponse response = new InterfaceControlsResponse();
            InterfaceControlRequest request = new InterfaceControlRequest();
            List<MInterfaceControl> interfaces = new List<MInterfaceControl>();

            eInterfaceControl.InterfaceControlId = eInterfaceControl.InterfaceControlId;

            request.MInterfaceControl = eInterfaceControl;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControl", "GetInterfaceControls", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    interfaces = response.InterfaceControls;
                }
            }

            return interfaces;
        }

        public MInterfaceControl GetInterfaceControl(MInterfaceControl EInterfaceControl, Session eSession)
        {
            MInterfaceControl interfaceE = new MInterfaceControl();
            InterfaceControlRequest request = new InterfaceControlRequest();
            InterfaceControlResponse response = new InterfaceControlResponse();

            request.MInterfaceControl = EInterfaceControl;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControl", "GetInterfaceControl", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    interfaceE = response.InterfaceControl;
                }
            }

            return interfaceE;
        }

        public string InsertInterfaceControl(MInterfaceControl EInterfaceControl, Session eSession)
        {
            InterfaceControlRequest request = new InterfaceControlRequest();
            InterfaceControlResponse response = new InterfaceControlResponse();
            string returnMsg = string.Empty;

            request.MInterfaceControl = EInterfaceControl;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControl", "InsertInterfaceControl", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking InterfaceControl api";
            }

            return returnMsg;
        }

        public string UpdateInterfaceControl(MInterfaceControl EInterfaceControl, Session eSession)
        {
            InterfaceControlRequest request = new InterfaceControlRequest();
            InterfaceControlResponse response = new InterfaceControlResponse();
            string returnMsg = string.Empty;

            request.MInterfaceControl = EInterfaceControl;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControl", "UpdateInterfaceControl", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking InterfaceControl api";
            }

            return returnMsg;
        }

        public string DeleteInterfaceControl(MInterfaceControl EInterfaceControl, Session eSession)
        {
            InterfaceControlRequest request = new InterfaceControlRequest();
            InterfaceControlResponse response = new InterfaceControlResponse();
            string returnMsg = string.Empty;

            request.MInterfaceControl = EInterfaceControl;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/InterfaceControl", "DeleteInterfaceControl", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceControlResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking InterfaceControl api";
            }

            return returnMsg;
        }
    }

    internal class InterfaceControlRequest
    {
        public MInterfaceControl MInterfaceControl { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class InterfaceControlsResponse
    {
        public List<MInterfaceControl> InterfaceControls { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class InterfaceControlResponse
    {
        public MInterfaceControl InterfaceControl { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
