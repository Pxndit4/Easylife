using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiTimeLineTestimonial
    {
        public List<MTimeLineTestimonial> GetTimeLineTestimonials(MTimeLineTestimonial MTimeLineTestimonial, Session Session)
        {

            TimeLineTestimonialsResponse response = new TimeLineTestimonialsResponse();
            TimeLineTestimonialRequest request = new TimeLineTestimonialRequest();
            List<MTimeLineTestimonial> Testimonials = new List<MTimeLineTestimonial>();

            MTimeLineTestimonial.TimeLineId = MTimeLineTestimonial.TimeLineId;

            request.testimonial = MTimeLineTestimonial;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonial", "GetTimeLineTestimonials", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialsResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    Testimonials = response.Testimonials;
                }
            }

            return Testimonials;
        }

        public MTimeLineTestimonial GetTimeLineTestimonial(MTimeLineTestimonial MTimeLineTestimonial, Session Session)
        {
            MTimeLineTestimonial timeLineE = new MTimeLineTestimonial();
            TimeLineTestimonialRequest request = new TimeLineTestimonialRequest();
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();

            request.testimonial = MTimeLineTestimonial;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonial", "GetTimeLineTestimonial", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    timeLineE = response.Testimonial;
                }
            }

            return timeLineE;
        }

        public string InsertTimeLineTestimonial(MTimeLineTestimonial MTimeLineTestimonial, Session Session)
        {
            TimeLineTestimonialRequest request = new TimeLineTestimonialRequest();
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            string returnMsg = string.Empty;

            request.testimonial = MTimeLineTestimonial;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonial", "InsertTimeLineTestimonial", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLineTestimonial api";
            }

            return returnMsg;
        }

        public string UpdateTimeLineTestimonial(MTimeLineTestimonial MTimeLineTestimonial, Session Session)
        {
            TimeLineTestimonialRequest request = new TimeLineTestimonialRequest();
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            string returnMsg = string.Empty;

            request.testimonial = MTimeLineTestimonial;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonial", "UpdatMTimeLineTestimonial", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLineTestimonial api";
            }

            return returnMsg;
        }

        public string DeleteTimeLineTestimonial(MTimeLineTestimonial MTimeLineTestimonial, Session Session)
        {
            TimeLineTestimonialRequest request = new TimeLineTestimonialRequest();
            TimeLineTestimonialResponse response = new TimeLineTestimonialResponse();
            string returnMsg = string.Empty;

            request.testimonial = MTimeLineTestimonial;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineTestimonial", "DeletMTimeLineTestimonial", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineTestimonialResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLineTestimonial api";
            }

            return returnMsg;
        }
    }

    internal class TimeLineTestimonialRequest
    {
        public MTimeLineTestimonial testimonial { get; set; }
        //public string Language { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class TimeLineTestimonialsResponse
    {
        public List<MTimeLineTestimonial> Testimonials { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class TimeLineTestimonialResponse
    {
        public MTimeLineTestimonial Testimonial { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
