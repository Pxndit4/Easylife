using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS
{
    public class WebApiLogLoad
    {
        public List<MLogLoad> GetLogsLoad(MLogLoad MLogLoad, Session eSession)
        {
            List<MLogLoad> list = new List<MLogLoad>();
            LogLoadRequest request = new LogLoadRequest();
            LogLoadListResponse response = new LogLoadListResponse();

            request.LogLoad = MLogLoad;
            request.Session = eSession;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("Project/api/logLoad", "GetLogsLoad", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<LogLoadListResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    list = response.LogLoadList;
                }
            }

            return list;
        }

    }


    [Serializable]
    public class LogLoadRequest : BaseRequest
    {
        public MLogLoad LogLoad { get; set; }
    }



    [Serializable]
    public class LogLoadListResponse : BaseResponse
    {
        public List<MLogLoad> LogLoadList { get; set; }

    }

}