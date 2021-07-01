using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiTimeLineMultimedia
    {
        public List<MTimeLineMultimedia> GetTimeLineMultimedias(MTimeLineMultimedia MTimeLineMultimedia, Session Session)
        {

            TimeLineMultimediasResponse response = new TimeLineMultimediasResponse();
            TimeLineMultimediaRequest request = new TimeLineMultimediaRequest();
            List<MTimeLineMultimedia> Multimedias = new List<MTimeLineMultimedia>();

            MTimeLineMultimedia.TimeLineId = MTimeLineMultimedia.TimeLineId;

            request.TimeLineMultimedia = MTimeLineMultimedia;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineMultimedia", "GetTimeLineMultimediaList", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineMultimediasResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    Multimedias = response.TimeLineMultimedias;
                }
            }

            return Multimedias;
        }

        public MTimeLineMultimedia GetTimeLineMultimedia(MTimeLineMultimedia MTimeLineMultimedia, Session Session)
        {
            MTimeLineMultimedia timeLineE = new MTimeLineMultimedia();
            TimeLineMultimediaRequest request = new TimeLineMultimediaRequest();
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();

            request.TimeLineMultimedia = MTimeLineMultimedia;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineMultimedia", "GetTimeLineMultimedia", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineMultimediaResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    timeLineE = response.TimeLineMultimedia;
                }
            }

            return timeLineE;
        }

        public string InsertTimeLineMultimedia(MTimeLineMultimedia MTimeLineMultimedia, Session objSession)
        {
            TimeLineMultimediaRequest request = new TimeLineMultimediaRequest();
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            string returnMsg = string.Empty;

            request.TimeLineMultimedia = MTimeLineMultimedia;
            request.Session = objSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineMultimedia", "InsertTimeLineMultimedia", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineMultimediaResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLineMultimedia api";
            }

            return returnMsg;
        }

        public string UpdateTimeLineMultimedia(MTimeLineMultimedia MTimeLineMultimedia, Session objSession)
        {
            TimeLineMultimediaRequest request = new TimeLineMultimediaRequest();
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            string returnMsg = string.Empty;

            request.TimeLineMultimedia = MTimeLineMultimedia;
            request.Session = objSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineMultimedia", "UpdatMTimeLineMultimedia", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineMultimediaResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLineMultimedia api";
            }

            return returnMsg;
        }

        public string DeleteTimeLineMultimedia(MTimeLineMultimedia MTimeLineMultimedia, Session Session)
        {
            TimeLineMultimediaRequest request = new TimeLineMultimediaRequest();
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            string returnMsg = string.Empty;

            request.TimeLineMultimedia = MTimeLineMultimedia;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/TimeLineMultimedia", "DeletMTimeLineMultimedia", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<TimeLineMultimediaResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking TimeLineMultimedia api";
            }

            return returnMsg;
        }
    }

    internal class TimeLineMultimediaRequest
    {
        public MTimeLineMultimedia TimeLineMultimedia { get; set; }
        //public string Language { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class TimeLineMultimediasResponse
    {
        public List<MTimeLineMultimedia> TimeLineMultimedias { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class TimeLineMultimediaResponse
    {
        public MTimeLineMultimedia TimeLineMultimedia { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
