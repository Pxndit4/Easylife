using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiBanner
    {
        public List<MBanner> GetBanners(MBanner MBanner, Session Session)
        {
            List<MBanner> banners = new List<MBanner>();
            BannerRequest request = new BannerRequest();
            BannersResponse response = new BannersResponse();

            MBanner.Title = MBanner.Title;
            MBanner.Status = MBanner.Status;

            request.Session = Session;
            request.banner = MBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Banner", "FilterBanners", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannersResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    banners = response.banners;
                }
            }

            return banners;
        }

        public MBanner GetBanner(MBanner MBanner, Session Session)
        {
            MBanner banner = new MBanner();
            BannerRequest request = new BannerRequest();
            BannerResponse response = new BannerResponse();

            request.Session = Session;
            request.banner = MBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Banner", "GetBanner", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    banner = response.banner;
                }
            }

            return banner;
        }

        public string InsertBanner(MBanner MBanner, Session Session)
        {
            BannerRequest request = new BannerRequest();
            BannerResponse response = new BannerResponse();
            string returnMsg = string.Empty;

            request.Session = Session;
            request.banner = MBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Banner", "InsertBanner", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Banner api";
            }

            return returnMsg;
        }

        public string UpdateBanner(MBanner MBanner, Session Session)
        {
            BannerRequest request = new BannerRequest();
            BannerResponse response = new BannerResponse();
            string returnMsg = string.Empty;

            request.Session = Session;
            request.banner = MBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Banner", "UpdateBanner", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Banner api";
            }

            return returnMsg;
        }

        public string DeleteBanner(MBanner MBanner, Session Session)
        {
            BannerRequest request = new BannerRequest();
            BannerResponse response = new BannerResponse();
            string returnMsg = string.Empty;

            request.Session = Session;
            request.banner = MBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Banner", "DeleteBanner", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Banner api";
            }

            return returnMsg;
        }
    }

    internal class BannerRequest
    {
        public MBanner banner { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class BannersResponse
    {
        public List<MBanner> banners { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class BannerResponse
    {
        public MBanner banner { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
