using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiTimeLineTranslate
    {
        public List<MTimeLineTranslate> GetTimeLineTranslates(MTimeLineTranslate eTimeLine, Session Session)
        {

            TimeLineTranslatesResponse response = new TimeLineTranslatesResponse();
            TimeLineTranslateRequest request = new TimeLineTranslateRequest();
            List<MTimeLineTranslate> TimeLines = new List<MTimeLineTranslate>();

            eTimeLine.TimeLineId = eTimeLine.TimeLineId;

            request.TimeLineTranslateBE = eTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTranslate", "GetTimeLineTranslates", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTranslatesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    TimeLines = response.TimeLineTranslates;
                }
            }

            return TimeLines;
        }

        public MTimeLineTranslate GetTimeLineTranslate(MTimeLineTranslate MTimeLineTranslate, Session Session)
        {
            MTimeLineTranslate projectTranslateE = new MTimeLineTranslate();
            TimeLineTranslateRequest request = new TimeLineTranslateRequest();
            TimeLineTranslateResponse response = new TimeLineTranslateResponse();

            request.TimeLineTranslateBE = MTimeLineTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTranslate", "GetTimeLineTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTranslateResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projectTranslateE = response.TimeLineTranslate;
                }
            }

            return projectTranslateE;
        }

        public string InsertTimeLineTranslate(MTimeLineTranslate MTimeLineTranslate, Session Session)
        {
            TimeLineTranslateRequest request = new TimeLineTranslateRequest();
            TimeLineTranslatesResponse response = new TimeLineTranslatesResponse();
            string returnMsg = string.Empty;

            request.TimeLineTranslateBE = MTimeLineTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTranslate", "InsertTimeLineTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string UpdateTimeLineTranslate(MTimeLineTranslate MTimeLineTranslate, Session Session)
        {
            TimeLineTranslateRequest request = new TimeLineTranslateRequest();
            TimeLineTranslateResponse response = new TimeLineTranslateResponse();
            string returnMsg = string.Empty;

            request.TimeLineTranslateBE = MTimeLineTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTranslate", "UpdatMTimeLineTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTranslateResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string DeleteTimeLineTranslate(MTimeLineTranslate MTimeLineTranslate, Session Session)
        {
            TimeLineTranslateRequest request = new TimeLineTranslateRequest();
            TimeLineTranslatesResponse response = new TimeLineTranslatesResponse();
            string returnMsg = string.Empty;

            request.TimeLineTranslateBE = MTimeLineTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTranslate", "DeletMTimeLineTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }
    }

    internal class TimeLineTranslateRequest
    {
        public MTimeLineTranslate TimeLineTranslateBE { get; set; }
        public Session Session { get; set; }

        public string ApplicationToken { get; set; }
    }

    internal class TimeLineTranslatesResponse
    {
        public List<MTimeLineTranslate> TimeLineTranslates { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class TimeLineTranslateResponse
    {
        public MTimeLineTranslate TimeLineTranslate { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
