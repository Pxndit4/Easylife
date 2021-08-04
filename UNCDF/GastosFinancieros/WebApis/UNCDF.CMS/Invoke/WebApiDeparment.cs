using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiDeparment
    {
        public List<MDeparment> FilDeparmentExclusion(MDeparment eProject)
        {
            List<MDeparment> projects = new List<MDeparment>();
            DeparmentRequest request = new DeparmentRequest();
            DeparmentsResponse response = new DeparmentsResponse();

            request.Deparment = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/projectExclusions", "FilDeparmentCodeExcluded", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DeparmentsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.Deparments;
                }
            }

            return projects;
        }

        public List<MDeparment> GetDeparments()
        {
            List<MDeparment> funds = new List<MDeparment>();
            BaseRequest request = new BaseRequest();
            DeparmentsResponse response = new DeparmentsResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/Deparment", "GetDeparments", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DeparmentsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    funds = response.Deparments;
                }
            }

            return funds;
        }

        public MDeparment GetDeparment(MDeparment ent)
        {
            MDeparment funds = new MDeparment();
            DeparmentRequest request = new DeparmentRequest();
            DeparmentResponse response = new DeparmentResponse();

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            request.Deparment = ent;

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/Deparment", "GetDeparment", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DeparmentResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    funds = response.Deparment;
                }
            }

            return funds;
        }

        public string InsertDeparment(List<MDeparment> list, Session eSession)
        {
            DeparmentsRequest request = new DeparmentsRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.Deparments = list;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("project/api/Deparment", "InsertDeparment", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }

        public string UpdateDeparment(MDeparment list, Session eSession)
        {
            DeparmentRequest request = new DeparmentRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.Deparment = list;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("project/api/Deparment", "UpdateDeparment", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }

    }

    [Serializable]
    public class DeparmentsResponse : BaseResponse
    {
        public List<MDeparment> Deparments { get; set; }
    }

    [Serializable]
    public class DeparmentResponse : BaseResponse
    {
        public MDeparment Deparment { get; set; }
    }

    [Serializable]
    public class DeparmentsRequest : BaseRequest
    {
        public List<MDeparment> Deparments { get; set; }
    }

    [Serializable]
    public class DeparmentRequest : BaseRequest
    {
        public MDeparment Deparment { get; set; }
    }
}