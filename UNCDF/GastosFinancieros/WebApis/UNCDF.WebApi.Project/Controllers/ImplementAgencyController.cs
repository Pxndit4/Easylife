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
    public class ImplementAgencyController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public ImplementAgencyController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetImplementAgencies")]
        public ImplementAgenciesResponse GetImplementAgencies([FromBody] BaseRequest request)
        {
            ImplementAgenciesResponse response = new ImplementAgenciesResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MImplementAgency> ImplementAgencies = BImplementAgency.List();

                response.ImplementAgencies = ImplementAgencies.ToArray();
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
        [Route("0/InsertImplementAgency")]
        public BaseResponse InsertImplementAgency([FromBody] ImplementAgenciesRequest request)
        {
            BaseResponse response = new BaseResponse();

            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.MaxValue;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
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
                    int ParamLOAD_DEPARM = 28; //IdParameter

                    BaseRequest baseRequest = new BaseRequest();

                    List<MImplementAgency> list = new List<MImplementAgency>();

                    int ValLoad = 0;

                    var res = BLogLoad.Insert(new MLogLoad
                    {
                        TypeParamId = ParamLOAD_DEPARM,
                        UserId = request.Session.UserId,
                        TotalCorrectRecords = request.TotalCorrect,
                        TotalBadRecords = request.TotalBad
                    }
                    , ref ValLoad);


                    var stringList = request.ImplementAgencies.OfType<MImplementAgency>();

                    

                    foreach (MImplementAgency model in request.ImplementAgencies)
                    {
                        MImplementAgency fund = new MImplementAgency();

                        fund.ImplementAgencyCode = model.ImplementAgencyCode;
                        fund.Description = model.Description;
                        fund.ShortDescription = model.ShortDescription;

                        list.Add(fund);
                    }

                    BImplementAgency.InsertAll(list);

                    //foreach (MImplementAgency model in request.ImplementAgencies)
                    //{
                    //    MImplementAgency fund = new MImplementAgency();

                    //    fund.ImplementAgencyCode = model.ImplementAgencyCode;
                    //    fund.Description = model.Description;
                    //    fund.ShortDescription = model.ShortDescription;

                    //    BImplementAgency.Insert(fund);
                    //}

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
