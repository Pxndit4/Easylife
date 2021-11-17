using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS
{
    public class WebApiSubscribers
    {
        public List<MSubscribers> GetSubscribers(MSubscribers MSubscribers, Session Session)
        {
            List<MSubscribers> lists = new List<MSubscribers>();
            SubscribersRequest request = new SubscribersRequest();
            SubscribersLisResponse response = new SubscribersLisResponse();

            request.Subscribers = MSubscribers;
            request.Session = Session;
            
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Subscribers", "GetSubscribers", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<SubscribersLisResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    lists = response.SubscribersList;
                }
            }

            return lists;
        }

    }

    internal class SubscribersRequest
    {
        public MSubscribers Subscribers { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class SubscribersLisResponse
    {
        public List<MSubscribers> SubscribersList { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

}
