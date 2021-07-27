using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS
{
    public class WebApiProjectExclusions
    {
        public string InsertProjectExlusions(MProjectExclusion ent, Session eSession)
        {
            ProjectExclusionRequest request = new ProjectExclusionRequest();
            ProjectExclusionResponse response = new ProjectExclusionResponse();
            string returnMsg = string.Empty;

            request.projectExclusion = ent;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "InsertProjectExclusion", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectExclusionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Project api";
            }

            return returnMsg;
        }

        public List<MProjectExclusion> ListProjectsCodeExclusions(MProjectExclusion eProject)
        {
            List<MProjectExclusion> projects = new List<MProjectExclusion>();
            ProjectExclusionRequest request = new ProjectExclusionRequest();
            ProjectExclusionsResponse response = new ProjectExclusionsResponse();

            request.projectExclusion = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "ListProjectsCodeExclusions", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectExclusionsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.projectExclusions;
                }
            }

            return projects;
        }

        public string DeleteProjectCode(MProjectExclusion mProjectCode, Session Session)
        {
            ProjectExclusionRequest request = new ProjectExclusionRequest();
            ProjectExclusionResponse response = new ProjectExclusionResponse();
            string returnMsg = string.Empty;

            request.Session = Session;
            request.projectExclusion = mProjectCode;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "DeleteProjectCode", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectExclusionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Project Exclusions api";
            }

            return returnMsg;
        }


    }


    public class ProjectExclusionsResponse : BaseResponse
    {
        public List<MProjectExclusion> projectExclusions { get; set; }
    }

    public class ProjectExclusionResponse : BaseResponse
    {
        public MProjectExclusion projectExclusion { get; set; }
    }

    [Serializable]
    public class ProjectExclusionRequest : BaseRequest
    {
        public MProjectExclusion projectExclusion { get; set; }
    }
}