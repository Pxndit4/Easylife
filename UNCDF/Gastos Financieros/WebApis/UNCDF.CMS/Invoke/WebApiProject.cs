using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS
{
    public class WebApiProject
    {
        public List<MProject> GetProjects(MProject eProject)
        {
            List<MProject> projects = new List<MProject>();
            ProjectsRequest request = new ProjectsRequest();
            ProjectsResponse response = new ProjectsResponse();

            request.Project = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("project/api/Project", "Getprojects", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.projects;
                }
            }

            return projects;
        }

        public string InsertProject(List<MProject> list, Session eSession)
        {
            ProjectsResponse request = new ProjectsResponse();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            //request.Project = list;
            //request.Session = eSession;
            //request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/project", "Insertproject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }
    }

    public class ProjectsResponse : BaseResponse
    {
        public List<MProject> projects { get; set; }
    }

    public class ProjectsRequest : BaseRequest
    {
        public MProject Project { get; set; }
    }
}
