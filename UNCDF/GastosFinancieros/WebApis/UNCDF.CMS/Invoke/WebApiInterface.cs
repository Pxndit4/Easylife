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
    public class WebApiInterface
    {
        public List<MInterface> GetInterfaces(MInterface eInterface, Session eSession)
        {

            InterfacesResponse response = new InterfacesResponse();
            InterfaceRequest request = new InterfaceRequest();
            List<MInterface> interfaces = new List<MInterface>();

            eInterface.InterfaceName = eInterface.InterfaceName;
            eInterface.Status = eInterface.Status;

            request.Interface = eInterface;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Interface", "FilterInterfaces", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<InterfacesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    interfaces = response.Interfaces;
                }
            }

            return interfaces;
        }

        public MInterface GetInterface(MInterface EInterface, Session eSession)
        {
            MInterface interfaceE = new MInterface();
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();

            request.Interface = EInterface;
            request.Session = eSession;
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

        public string InsertInterface(MInterface EInterface, Session eSession)
        {
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();
            string returnMsg = string.Empty;

            request.Interface = EInterface;
            request.Session = eSession;
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

        public string UpdateInterface(MInterface EInterface, Session eSession)
        {
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();
            string returnMsg = string.Empty;

            request.Interface = EInterface;
            request.Session = eSession;
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

        public string DeleteInterface(MInterface EInterface, Session eSession)
        {
            InterfaceRequest request = new InterfaceRequest();
            InterfaceResponse response = new InterfaceResponse();
            string returnMsg = string.Empty;

            request.Interface = EInterface;
            request.Session = eSession;
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
