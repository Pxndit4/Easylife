using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class WebApiCountry
    {
        public List<MCountry> GetCountries(MCountry MBanner, Session Session)
        {
            List<MCountry> countries = new List<MCountry>();
            CountryRequest request = new CountryRequest();
            CountriesResponse response = new CountriesResponse();

            MBanner.Continents = "";
            MBanner.Status = MBanner.Status;

            request.Session = Session;
            request.Country = MBanner;
            request.ApplicationToken = ConfigurationManager.AppSettings["ApplicationToken"].ToString();

            string bodyrequest = JsonConvert.SerializeObject(request);
            string statuscode = string.Empty;
            string bodyresponse = new Helper().InvokeApi("appconfig/api/Country", "GetCountries", bodyrequest, ref statuscode);

            if (statuscode.Equals("OK"))
            {
                response = JsonConvert.DeserializeObject<CountriesResponse>(bodyresponse);

                if (response.Code.Equals("0"))
                {
                    countries = response.Countries;
                }
            }

            return countries;
        }
    }

    internal class CountryRequest
    {
        public MCountry Country { get; set; }
        public Session Session { get; set; }
        public string ApplicationToken { get; set; }
    }

    internal class CountriesResponse
    {
        public List<MCountry> Countries { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}