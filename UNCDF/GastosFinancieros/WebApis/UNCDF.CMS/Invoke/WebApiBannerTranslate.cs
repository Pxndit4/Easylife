using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiBannerTranslate

    {
        public List<MBannerTranslate> GetBannerTranslates(MBannerTranslate eBanner, Session Session)
        {

            BannerTranslatesResponse response = new BannerTranslatesResponse();
            BannerTranslateRequest request = new BannerTranslateRequest();
            List<MBannerTranslate> genders = new List<MBannerTranslate>();

            //eBanner.BannerId = eBanner.BannerId;

            request.MBannerTranslate = eBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/BannerTranslate", "GetBannerTranslates", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerTranslatesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genders = response.BannerTranslates;
                }
            }

            return genders;
        }

        public MBannerTranslate GetBannerTranslate(MBannerTranslate MBannerTranslate, Session Session)
        {
            MBannerTranslate genderE = new MBannerTranslate();
            BannerTranslateRequest request = new BannerTranslateRequest();
            BannerTranslateResponse response = new BannerTranslateResponse();

            request.MBannerTranslate = MBannerTranslate;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/BannerTranslate", "GetBannerTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerTranslateResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    genderE = response.BannerTranslate;
                }
            }

            return genderE;
        }

        public string InsertBannerTranslate(MBannerTranslate MBannerTranslate, Session Session)
        {
            BannerTranslateRequest request = new BannerTranslateRequest();
            BannerTranslatesResponse response = new BannerTranslatesResponse();
            string returnMsg = string.Empty;

            request.MBannerTranslate = MBannerTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/BannerTranslate", "InsertBannerTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Banner api";
            }

            return returnMsg;
        }

        public string UpdateBannerTranslate(MBannerTranslate MBannerTranslate, Session Session)
        {
            BannerTranslateRequest request = new BannerTranslateRequest();
            BannerTranslateResponse response = new BannerTranslateResponse();
            string returnMsg = string.Empty;

            request.MBannerTranslate = MBannerTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/BannerTranslate", "UpdateBannerTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerTranslateResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Banner api";
            }

            return returnMsg;
        }

        public string DeleteBanner(MBannerTranslate MBannerTranslate, Session Session)
        {
            BannerTranslateRequest request = new BannerTranslateRequest();
            BannerTranslatesResponse response = new BannerTranslatesResponse();
            string returnMsg = string.Empty;

            request.MBannerTranslate = MBannerTranslate;
            request.Session = Session;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/BannerTranslate", "DeleteBannerTranslate", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<BannerTranslatesResponse>(bodyresponse);
                returnMsg = response.Code + "|" + response.Message;
            }
            else
            {
                returnMsg = "2" + "|" + "Error invoking Banner api";
            }

            return returnMsg;
        }
    }

    internal class BannerTranslateRequest
    {
        public MBannerTranslate MBannerTranslate { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class BannerTranslatesResponse
    {
        public List<MBannerTranslate> BannerTranslates { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    internal class BannerTranslateResponse
    {
        public MBannerTranslate BannerTranslate { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
