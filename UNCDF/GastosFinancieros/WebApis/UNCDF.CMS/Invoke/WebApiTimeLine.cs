using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiTimeLine
    {
        public List<MTimeLine> GetTimeLines(MTimeLine MTimeLine, Session Session)
        {

            TimeLinesResponse response = new TimeLinesResponse();
            TimeLineRequest request = new TimeLineRequest();
            List<MTimeLine> timeLines = new List<MTimeLine>();

            MTimeLine.ProjectId = MTimeLine.ProjectId;


            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "FilterTimeLines", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLinesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    timeLines = response.TimeLines;
                }
            }

            return timeLines;
        }

        public MTimeLine GetTimeLine(MTimeLine MTimeLine, Session Session)
        {
            MTimeLine timeLineE = new MTimeLine();
            TimeLineRequest request = new TimeLineRequest();
            TimeLineResponse response = new TimeLineResponse();

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "GetTimeLine", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    timeLineE = response.TimeLine;
                }
            }

            return timeLineE;
        }

        public string InsertTimeLine(MTimeLine MTimeLine, Session Session)
        {
            TimeLineRequest request = new TimeLineRequest();
            TimeLineResponse response = new TimeLineResponse();


            string returnMsg = string.Empty;

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "InsertTimeLine", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string UpdateTimeLine(MTimeLine MTimeLine, Session Session)
        {
            TimeLineRequest request = new TimeLineRequest();
            TimeLineResponse response = new TimeLineResponse();
            string returnMsg = string.Empty;

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "UpdateTimeLine", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string DeleteTimeLine(MTimeLine MTimeLine, Session Session)
        {
            TimeLineRequest request = new TimeLineRequest();
            TimeLineResponse response = new TimeLineResponse();
            string returnMsg = string.Empty;

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "DeleteTimeLine", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public List<MTimeLine> GetTimeLinesUnApproved(MTimeLine MTimeLine, Session Session)
        {

            TimeLinesResponse response = new TimeLinesResponse();
            TimeLineRequest request = new TimeLineRequest();
            List<MTimeLine> timeLines = new List<MTimeLine>();

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "GetTimeLineUnApproved", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLinesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    timeLines = response.TimeLines;
                }
            }

            return timeLines;
        }
        public string Approved(MTimeLine MTimeLine, Session Session)
        {
            TimeLineRequest request = new TimeLineRequest();
            TimeLineResponse response = new TimeLineResponse();
            string returnMsg = string.Empty;

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "TimeLineApproved", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string Reject(MTimeLine MTimeLine, Session Session)
        {
            TimeLineRequest request = new TimeLineRequest();
            TimeLineResponse response = new TimeLineResponse();
            string returnMsg = string.Empty;

            request.timeLine = MTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLine", "TimeLineReject", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }


    }

    internal class TimeLineRequest
    {
        public MTimeLine timeLine { get; set; }
        //public string Language { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class TimeLinesResponse
    {
        public List<MTimeLine> TimeLines { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class TimeLineResponse
    {
        public MTimeLine TimeLine { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
