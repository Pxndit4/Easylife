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
            string bodyresponse = new Helper().InvokeApi("Project/api/project", "Getprojects", bodyrequest, ref statuscode);

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

        public MProject GetProject(MProject EProject , Session eSession)
        {
            MProject project = new MProject();
            ProjectsRequest request = new ProjectsRequest();
            ProjectResponse response = new ProjectResponse();

            request.Project = EProject;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/project", "GetProject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    project = response.Project;
                }
            }

            return project;
        }

        public string InsertProject(List<MProject> list, Session eSession)
        {
            //ProjectsResponse request = new ProjectsResponse();
            ProjectsListRequest request = new ProjectsListRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.Projects = list;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/project", "InsertProject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BaseResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }

            return returnMsg;
        }

        public string UpdateProject(MProject EProject, Session eSession)
        {
            ProjectsRequest request = new ProjectsRequest();
            ProjectResponse response = new ProjectResponse();
            string returnMsg = string.Empty;

            request.Project = EProject;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/project", "UpdateProject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Project api";
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

    public class ProjectsListRequest : BaseRequest
    {
        public List<MProject> Projects { get; set; }
    }

    public class ProjectResponse : BaseResponse
    {
        public MProject Project { get; set; }
    }
}
