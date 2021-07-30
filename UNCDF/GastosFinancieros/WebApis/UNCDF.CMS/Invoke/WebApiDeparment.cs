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

    }


    [Serializable]
    public class DeparmentsResponse : BaseResponse
    {
        public List<MDeparment> Deparments { get; set; }
    }


    [Serializable]
    public class DeparmentsRequest : BaseRequest
    {
        public MDeparment[] Deparments { get; set; }
    }

    [Serializable]
    public class DeparmentRequest : BaseRequest
    {
        public MDeparment Deparment { get; set; }
    }
}