using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiTimeLineTestimonialTranslate
    {
        public List<MTimeLineTestimonialTranslate> GetTimeLineTestimonialTranslates(MTimeLineTestimonialTranslate eTimeLine, Session Session)
        {

            TimeLineTestimonialTranslatesResponse response = new TimeLineTestimonialTranslatesResponse();
            TimeLineTestimonialTranslateRequest request = new TimeLineTestimonialTranslateRequest();
            List<MTimeLineTestimonialTranslate> TimeLines = new List<MTimeLineTestimonialTranslate>();

            request.TimeLineTestimonialTranslate = eTimeLine;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonialTranslate", "GetTimeLineTestimonialTranslates", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialTranslatesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    TimeLines = response.TimeLineTestimonialTranslates;
                }
            }

            return TimeLines;
        }

        public MTimeLineTestimonialTranslate GetTimeLineTestimonialTranslate(MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate, Session Session)
        {
            MTimeLineTestimonialTranslate projectTranslateE = new MTimeLineTestimonialTranslate();
            TimeLineTestimonialTranslateRequest request = new TimeLineTestimonialTranslateRequest();
            TimeLineTestimonialTranslateResponse response = new TimeLineTestimonialTranslateResponse();

            request.TimeLineTestimonialTranslate = MTimeLineTestimonialTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonialTranslate", "GetTimeLineTestimonialTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialTranslateResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    projectTranslateE = response.TimeLineTestimonialTranslate;
                }
            }

            return projectTranslateE;
        }

        public string InsertTimeLineTestimonialTranslate(MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate, Session Session)
        {
            TimeLineTestimonialTranslateRequest request = new TimeLineTestimonialTranslateRequest();
            TimeLineTestimonialTranslatesResponse response = new TimeLineTestimonialTranslatesResponse();
            string returnMsg = string.Empty;

            request.TimeLineTestimonialTranslate = MTimeLineTestimonialTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonialTranslate", "InsertTimeLineTestimonialTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string UpdateTimeLineTestimonialTranslate(MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate, Session Session)
        {
            TimeLineTestimonialTranslateRequest request = new TimeLineTestimonialTranslateRequest();
            TimeLineTestimonialTranslateResponse response = new TimeLineTestimonialTranslateResponse();
            string returnMsg = string.Empty;

            request.TimeLineTestimonialTranslate = MTimeLineTestimonialTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonialTranslate", "UpdatMTimeLineTestimonialTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialTranslateResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }

        public string DeleteTimeLineTestimonialTranslate(MTimeLineTestimonialTranslate MTimeLineTestimonialTranslate, Session Session)
        {
            TimeLineTestimonialTranslateRequest request = new TimeLineTestimonialTranslateRequest();
            TimeLineTestimonialTranslatesResponse response = new TimeLineTestimonialTranslatesResponse();
            string returnMsg = string.Empty;

            request.TimeLineTestimonialTranslate = MTimeLineTestimonialTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonialTranslate", "DeletMTimeLineTestimonialTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLine api";
            }

            return returnMsg;
        }
    }

    internal class TimeLineTestimonialTranslateRequest
    {
        public MTimeLineTestimonialTranslate TimeLineTestimonialTranslate { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class TimeLineTestimonialTranslatesResponse
    {
        public List<MTimeLineTestimonialTranslate> TimeLineTestimonialTranslates { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class TimeLineTestimonialTranslateResponse
    {
        public MTimeLineTestimonialTranslate TimeLineTestimonialTranslate { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
