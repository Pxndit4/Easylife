using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiProjectFinancial
    {
        public List<MProjectFinancials> GetProjectFinancials(MProjectFinancials eProject)
        {
            List<MProjectFinancials> projects = new List<MProjectFinancials>();
            ProjectFinancialRequest request = new ProjectFinancialRequest();
            ProjectFinancialResponse response = new ProjectFinancialResponse();

            request.ProjectFinancial = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectFinancial", "GetProjectFinancials", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectFinancialResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.ProjectFinancials;
                }
            }

            return projects;
        }

        public string InsertProjectFinancial(List<MProjectFinancials> list, Session eSession)
        {
            ProjectFinancialsRequest request = new ProjectFinancialsRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.ProjectFinancials = list;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectFinancial", "InsertProjectFinancial", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }

        public string InsertProjectFinancialHistory(List<MProjectFinancials> list, Session eSession)
        {
            ProjectFinancialsRequest request = new ProjectFinancialsRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.ProjectFinancials = list;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectFinancial", "InsertProjectFinancialHistory", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }


        
    }

    [Serializable]
    public class ProjectFinancialResponse : BaseResponse
    {
        public List<MProjectFinancials> ProjectFinancials { get; set; }
    }

    
    [Serializable]
    public class ProjectFinancialsRequest : BaseRequest
    {
        public List<MProjectFinancials> ProjectFinancials { get; set; }
    }
    public class ProjectFinancialRequest : BaseRequest
    {
        public MProjectFinancials ProjectFinancial { get; set; }
    }
}