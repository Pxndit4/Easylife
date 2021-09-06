using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiUserProject
    {
        public List<MUserProject> GetUserProjectList(MUserProject MProfile)
        {
            List<MUserProject> profiles = new List<MUserProject>();
            UserProjectRequest request = new UserProjectRequest();
            UserProjectsResponse response = new UserProjectsResponse();

            request.UserProject = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/UserProject", "GetUserProjectList", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserProjectsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    profiles = response.UserProjects;
                }
            }

            return profiles;
        }

        public List<MUserProject> GetAssignedList(MUserProject MProfile)
        {
            List<MUserProject> profiles = new List<MUserProject>();
            UserProjectRequest request = new UserProjectRequest();
            UserProjectsResponse response = new UserProjectsResponse();

            request.UserProject = MProfile;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/UserProject", "GetAssignedList", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserProjectsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    profiles = response.UserProjects;
                }
            }

            return profiles;
        }

        public string RegisterUserProject(MUserProject MGender, Session Session)
        {
            UserProjectRequest request = new UserProjectRequest();
            UserProjectResponse response = new UserProjectResponse();
            string returnMsg = string.Empty;

            request.UserProject = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/UserProject", "RegisterUserProject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserProjectResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }


        public string DeleteUserProject(MUserProject MGender, Session Session)
        {
            UserProjectRequest request = new UserProjectRequest();
            UserProjectResponse response = new UserProjectResponse();
            string returnMsg = string.Empty;

            request.UserProject = MGender;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/UserProject", "DeleteUserProject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<UserProjectResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Gender api";
            }

            return returnMsg;
        }


    }
    public class UserProjectsResponse : BaseResponse
    {
        public List<MUserProject> UserProjects { get; set; }
    }


    public class UserProjectResponse : BaseResponse
    {
        public MUserProject UserProject { get; set; }
    }



    public class UserProjectRequest : BaseRequest
    {
        public MUserProject UserProject { get; set; }

    }
}