using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class CountryController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsS3 _MAwsS3;

        public CountryController(
            IHostingEnvironment env,
            IOptions<AppSettings> appSettings,
            IOptions<MAwsS3> MAwsS3
        )
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsS3 = MAwsS3.Value;
        }


        [HttpPost]
        [Route("0/GetContinents")]
        public ContinentsResponse GetContinents([FromBody] ContinentRequest request)
        {
            ContinentsResponse response = new ContinentsResponse();
            List<MContinent> continents = new List<MContinent>();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MContinent continent = new MContinent();

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            continent.Description = request.Continent.Description;
            continent.Status = request.Continent.Status;

            int Val = 0;

            continents = BContinent.Lis(continent, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Continent");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Continent");
            }

            response.Continents = continents;

            return response;
        }

        [HttpPost]
        [Route("0/GetCountries")]
        public CountriesResponse GetCountries([FromBody] CountryRequest request)
        {
            CountriesResponse response = new CountriesResponse();
            List<MCountry> countries = new List<MCountry>();
            string CountryPath = _appSettings.Value.CountryPath;

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MCountry country = new MCountry();

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            country.Continents = request.Country.Continents;            

            int Val = 0;

            //countries = CountryBN.List(country, ref Val);
            countries = BCountry.List(country, ref Val).Select(x => new MCountry
            {
                CountryId = x.CountryId,
                Description = x.Description,
                Flag = x.Flag.Replace(CountryPath, string.Empty),
                Prefix = x.Prefix,
                ContinentId = x.ContinentId,
                ContinentName = x.ContinentName,
                Status = x.Status
            }).ToList();


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Country");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Country");
            }

            response.Countries = countries;

            return response;
        }
    }
}
