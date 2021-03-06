using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace UNCDF.CMS
{
    public class Helper
    {
        public string InvokeApi(string path, string method, string requestBodyString, ref string statusCode)
        {
            string requestURL = ConfigurationManager.AppSettings["URLServices"].ToString() + path + ConfigurationManager.AppSettings["VersionServices"].ToString() + method;

            string responseBody = "";
            
            /*
            if (path.Contains("security"))
            {
                path = path.Replace("security/", "");
                requestURL = ConfigurationManager.AppSettings["URLServicesSecurity"].ToString() + path + ConfigurationManager.AppSettings["VersionServices"].ToString() + method;
            }


            if (path.Contains("Project") || path.Contains("project"))
            {
                path = path.Replace("Project/", "");
                path = path.Replace("project/", "");
                requestURL = ConfigurationManager.AppSettings["URLServicesproject"].ToString() + path + ConfigurationManager.AppSettings["VersionServices"].ToString() + method;
            }

            if (path.Contains("appconfig/"))
            {
                path = path.Replace("appconfig/", "");
                requestURL = ConfigurationManager.AppSettings["URLServicesconfig"].ToString() + path + ConfigurationManager.AppSettings["VersionServices"].ToString() + method;
            }


            if (path.Contains("donation/"))
            {
                path = path.Replace("donation/", "");
                requestURL = ConfigurationManager.AppSettings["URLServicesDonati"].ToString() + path + ConfigurationManager.AppSettings["VersionServices"].ToString() + method;
            }
            */
            

            HttpWebRequest request = WebRequest.Create(requestURL) as HttpWebRequest;
            
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);

            request.Method = "POST";

            request.ContentType = "application/json";
            request.Accept = "application/json";
            // Load the body for the post request
            var requestStringBytes = Encoding.UTF8.GetBytes(requestBodyString);
            request.GetRequestStream().Write(requestStringBytes, 0, requestStringBytes.Length);


            try
            {
                // Make the call
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    statusCode = response.StatusCode.ToString();
                    using (var reader = new StreamReader(response.GetResponseStream(), ASCIIEncoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response is HttpWebResponse)
                {
                    HttpWebResponse response = (HttpWebResponse)e.Response;
                    statusCode = response.StatusCode.ToString();
                    using (var reader = new StreamReader(response.GetResponseStream(), ASCIIEncoding.ASCII))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }

            return responseBody;

        }
    }
}