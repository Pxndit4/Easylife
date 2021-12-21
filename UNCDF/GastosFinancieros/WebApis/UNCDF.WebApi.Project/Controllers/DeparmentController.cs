using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UNCDF.Layers.Model;
using UNCDF.Layers.Business;
using UNCDF.Utilities;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class DeparmentController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public DeparmentController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetDeparments")]
        public DeparmentsResponse GetDeparments([FromBody] BaseRequest request)
        {
            DeparmentsResponse response = new DeparmentsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MDeparment> deparments = BDeparment.List();

                response.Deparments = deparments.ToArray();
                response.Code = "0";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }

        [HttpPost]
        [Route("0/GetDeparment")]
        public DeparmentResponse GetDeparment([FromBody] DeparmentRequest request)
        {
            DeparmentResponse response = new DeparmentResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                MDeparment ent = new MDeparment();

                ent.DeparmentId = request.Deparment.DeparmentId;

                MDeparment deparment = BDeparment.Get(ent);

                response.Deparment = deparment;
                response.Code = "0";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }

        [HttpPost]
        [Route("0/InsertDeparment")]
        public BaseResponse InsertDeparment([FromBody] DeparmentsRequest request)
        {
            BaseResponse response = new BaseResponse();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                    {
                        response.Code = "2";
                        response.Message = Messages.ApplicationTokenNoAutorize;
                        return response;
                    }

                    string webRoot = _env.ContentRootPath;
                    string rootPath = _appSettings.Value.rootPath;
                    string ProjectPath = _appSettings.Value.ProjectPath;
                    
                    int ParamLOAD_DEPARM = 29; //IdParameter

                    BaseRequest baseRequest = new BaseRequest();
                    
                    int ValLoad = 0;

                    var res = BLogLoad.Insert(new MLogLoad
                    {
                        TypeParamId = ParamLOAD_DEPARM,
                        UserId = request.Session.UserId,
                        TotalCorrectRecords = request.TotalCorrect,
                        TotalBadRecords = request.TotalBad
                    }
                    , ref ValLoad);

                    foreach (MDeparment model in request.Deparments)
                    {
                        MDeparment Deparment = new MDeparment();

                        Deparment.DeparmentCode = model.DeparmentCode;
                        Deparment.Description = model.Description;
                        Deparment.PracticeArea = model.PracticeArea;
                        Deparment.Region = model.Region;

                        BDeparment.Insert(Deparment);
                    }

                    scope.Complete();
                    response.Code = "0";
                    response.Message = "Success";
                }
                catch (Exception ex)
                {
                    response.Code = "2";
                    response.Message = ex.Message;

                    scope.Dispose();
                }
            }

            return response;
        }

        [HttpPost]
        [Route("0/UpdateDeparment")]
        public BaseResponse UpdateDeparment([FromBody] DeparmentRequest request)
        {
            BaseResponse response = new BaseResponse();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                    {
                        response.Code = "2";
                        response.Message = Messages.ApplicationTokenNoAutorize;
                        return response;
                    }

                    string webRoot = _env.ContentRootPath;
                    string rootPath = _appSettings.Value.rootPath;
                    string ProjectPath = _appSettings.Value.ProjectPath;

                    BaseRequest baseRequest = new BaseRequest();


                    MDeparment Deparment = new MDeparment();

                    Deparment.DeparmentId = request.Deparment.DeparmentId;
                    Deparment.Longitude = request.Deparment.Longitude;
                    Deparment.Latitude = request.Deparment.Latitude;
                    Deparment.CountryId = request.Deparment.CountryId;

                    BDeparment.Update(Deparment);

                    scope.Complete();
                    response.Code = "0";
                    response.Message = "Success";
                }
                catch (Exception ex)
                {
                    response.Code = "2";
                    response.Message = ex.Message;

                    scope.Dispose();
                }
            }

            return response;
        }
    }
}
