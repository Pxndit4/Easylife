using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class CountriesResponse : BaseResponse
    {
        public List<MCountry> Countries { get; set; }
    }

    [Serializable]
    public class CountryRequest : BaseRequest
    {
        public MCountry Country { get; set; }
    }

    [Serializable]
    public class ContinentRequest : BaseRequest
    {
        public MContinent Continent { get; set; }
    }

    [Serializable]
    public class ContinentsResponse : BaseResponse
    {
        public List<MContinent> Continents { get; set; }
    }
}
