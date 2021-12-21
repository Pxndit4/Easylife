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

        public string InsertProjectFinancial(List<MProjectFinancials> list, int TotalCorrect, int TotalBad, Session eSession)
        {
            ProjectFinancialsRequest request = new ProjectFinancialsRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.ProjectFinancials = list;
            request.Session = eSession;

            request.TotalCorrect = TotalCorrect;
            request.TotalBad = TotalBad;

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

        public string InsertProjectFinancialHistory(List<MProjectFinancials> list, int TotalCorrect, int TotalBad, Session eSession)
        {
            ProjectFinancialsRequest request = new ProjectFinancialsRequest();
            BaseResponse response = new BaseResponse();
            string returnMsg = string.Empty;

            request.ProjectFinancials = list;
            request.TotalCorrect = TotalCorrect;
            request.TotalBad = TotalBad;
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
        public List<MProjectFinancials> GetProjectFinancialValidYear(string years, Session eSession)
        {
            ProjectFinancialYearRequest request = new ProjectFinancialYearRequest();
            //ProjectFinancialYearResponse response = new ProjectFinancialYearResponse();
            ProjectFinancialResponse response = new ProjectFinancialResponse();
            List<MProjectFinancials> projects = new List<MProjectFinancials>();

            request.Year = years;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/ProjectFinancial", "GetProjectFinancialValidYear", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<ProjectFinancialResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projects = response.ProjectFinancials;
                }
            }



            //if (statuscode.Equals("OK"))
            //{
            //    response = JsonConvert.DeserializeObject<ProjectFinancialYearResponse>(bodyresponse);
            //    //returnMsg = response.Code + "|" + response.Message;
            //    returnYears = response.Years;
            //}

            return projects;
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
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public List<MProjectFinancials> ProjectFinancials { get; set; }
    }
    public class ProjectFinancialRequest : BaseRequest
    {
        public MProjectFinancials ProjectFinancial { get; set; }
    }

    [Serializable]
    public class ProjectFinancialYearResponse : BaseResponse
    {
        public string[] Years { get; set; }
    }

    public class ProjectFinancialYearRequest : BaseRequest
    {
        public string Year { get; set; }
    }


}