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
    public class WebApiInterface
    {
        public MInterface GetInterface(MInterface MInterface, Session Session)
        {
            MInterface interfaceE = new MInterface();
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();

            request.Interface = MInterface;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Interface", "GetInterface", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    interfaceE = response.Interface;
                }
            }

            return interfaceE;
        }

        public string InsertInterface(MInterface MInterface, Session Session)
        {
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();
            string returnMsg = string.Empty;

            request.Interface = MInterface;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Interface", "InsertInterface", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Interface api";
            }

            return returnMsg;
        }

        public string UpdateInterface(MInterface MInterface, Session Session)
        {
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();
            string returnMsg = string.Empty;

            request.Interface = MInterface;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Interface", "UpdateInterface", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Interface api";
            }

            return returnMsg;
        }

        public string DeleteInterface(MInterface MInterface, Session Session)
        {
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();
            string returnMsg = string.Empty;

            request.Interface = MInterface;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Interface", "DeleteInterface", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfaceResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Interface api";
            }

            return returnMsg;
        }
    }

    internal class InterfaceRequest
    {
        public MInterface Interface { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class InterfacesResponse
    {
        public List<MInterface> Interfaces { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class InterfaceResponse
    {
        public MInterface Interface { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
