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

        public string InsertDeparmentExlusions(MDeparmentExclusion ent, Session eSession)
        {
            DeparmentExclusionRequest request = new DeparmentExclusionRequest();
            DeparmentExclusionResponse response = new DeparmentExclusionResponse();
            string returnMsg = string.Empty;

            request.deparmentExclusion = ent;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "InsertDeparmentExclusion", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DeparmentExclusionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Deparment Code api";
            }

            return returnMsg;
        }

        public List<MDeparmentExclusion> ListDeparmentCodeExclusions(MDeparmentExclusion eProject)
        {
            List<MDeparmentExclusion> projects = new List<MDeparmentExclusion>();
            DeparmentExclusionRequest request = new DeparmentExclusionRequest();
            DeparmentExclusionsResponse response = new DeparmentExclusionsResponse();

            request.deparmentExclusion = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "ListDeparmentCodeExclusions", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DeparmentExclusionsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.departementExclusions;
                }
            }

            return projects;
        }


        //public List<MDeparment> FilDeparmentCodeExcluded(MDeparment eProject)
        //{
        //    List<MDeparment> projects = new List<MDeparment>();
        //    DeparmentExclusionRequest request = new DeparmentExclusionRequest();
        //    DeparmentExclusionsResponse response = new DeparmentExclusionsResponse();

        //    request.deparmentExclusion = eProject;

        //    request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

        //    string bodyrequest = JsonConvert.SerializeObject(request);
        //    string statuscode = string.Empty;
        //    string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "ListProjectsCodeExclusions", bodyrequest, ref statuscode);

        //    if (statuscode.Equals("OK"))
        //    {
        //        response = JsonConvert.DeserializeObject<DeparmentExclusionsResponse>(bodyresponse);

        //        if (response.Code.Equals("0"))
        //        {
        //            projects = response.departementExclusions;
        //        }
        //    }

        //    return projects;
        //}


        public string DeleteDeparmentCode(MDeparmentExclusion mProjectCode, Session Session)
        {
            DeparmentExclusionRequest request = new DeparmentExclusionRequest();
            DeparmentExclusionResponse response = new DeparmentExclusionResponse();
            string returnMsg = string.Empty;

            request.Session = Session;
            request.deparmentExclusion = mProjectCode;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "DeleteDeparmentCode", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<DeparmentExclusionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Deparment Exclusions api";
            }

            return returnMsg;
        }


        public string InsertPracticeAreaExlusions(MPracticeAreaExclusion ent, Session eSession)
        {
            PracticeAreaExclusionRequest request = new PracticeAreaExclusionRequest();
            PracticeAreaExclusionResponse response = new PracticeAreaExclusionResponse();
            string returnMsg = string.Empty;

            request.practiceAreaExclusion = ent;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "InsertPracticeAreaExclusion", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<PracticeAreaExclusionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Practice Area Code api";
            }

            return returnMsg;
        }

        public List<MPracticeAreaExclusion> ListPracticeAreasExclusions(MPracticeAreaExclusion eProject)
        {
            List<MPracticeAreaExclusion> projects = new List<MPracticeAreaExclusion>();
            PracticeAreaExclusionRequest request = new PracticeAreaExclusionRequest();
            PracticeAreasResponse response = new PracticeAreasResponse();

            request.practiceAreaExclusion = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "ListPracticeAreasExclusions", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<PracticeAreasResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.practiceAreaExclusions;
                }
            }

            return projects;
        }

        public string DeletePracticeArea(MPracticeAreaExclusion mProjectCode, Session Session)
        {
            PracticeAreaExclusionRequest request = new PracticeAreaExclusionRequest();
            PracticeAreaExclusionResponse response = new PracticeAreaExclusionResponse();
            string returnMsg = string.Empty;

            request.Session = Session;
            request.practiceAreaExclusion = mProjectCode;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "DeletePracticeAreaExclusion", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<PracticeAreaExclusionResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Deparment Exclusions api";
            }

            return returnMsg;
        }

        public List<MPracticeAreaExclusion> FilPracticeAreasExclusions(MPracticeAreaExclusion eProject)
        {
            List<MPracticeAreaExclusion> projects = new List<MPracticeAreaExclusion>();
            PracticeAreaExclusionRequest request = new PracticeAreaExclusionRequest();
            PracticeAreasResponse response = new PracticeAreasResponse();

            request.practiceAreaExclusion = eProject;

            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectExclusions", "FilPracticeAreasExclusions", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<PracticeAreasResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.practiceAreaExclusions;
                }
            }

            return projects;
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




    public class DeparmentExclusionsResponse : BaseResponse
    {
        public List<MDeparmentExclusion> departementExclusions { get; set; }
    }

    public class DeparmentExclusionResponse : BaseResponse
    {
        public MDeparmentExclusion deparmentExclusion { get; set; }
    }

    [Serializable]
    public class DeparmentExclusionRequest : BaseRequest
    {
        public MDeparmentExclusion deparmentExclusion { get; set; }
    }


    public class PracticeAreasResponse : BaseResponse
    {
        public List<MPracticeAreaExclusion> practiceAreaExclusions { get; set; }
    }

    public class PracticeAreaExclusionResponse : BaseResponse
    {
        public MPracticeAreaExclusion practiceAreaExclusion { get; set; }
    }

    [Serializable]
    public class PracticeAreaExclusionRequest : BaseRequest
    {
        public MPracticeAreaExclusion practiceAreaExclusion { get; set; }
    }
}