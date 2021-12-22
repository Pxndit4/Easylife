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
    public class DonorPartnerController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public DonorPartnerController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetDonorPartners")]
        public DonorPartnersResponse GetDonorPartners([FromBody] BaseRequest request)
        {
            DonorPartnersResponse response = new DonorPartnersResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MDonorPartner> donorpartners = BDonorPartner.List();

                response.DonorPartners = donorpartners.ToArray();
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
        [Route("0/InsertDonorPartner")]
        public BaseResponse InsertDonorPartner([FromBody] DonorPartnersRequest request)
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
                    
                    int ParamLOAD_DONORPAR = 31; //IdParameter
               
                    BaseRequest baseRequest = new BaseRequest();
                    
                    int ValLoad = 0;
                    
                    var res =  BLogLoad.Insert(new MLogLoad {  
                        TypeParamId         = ParamLOAD_DONORPAR,
                        UserId              = request.Session.UserId,
                        TotalCorrectRecords = request.TotalCorrect,
                        TotalBadRecords     = request.TotalBad
                    }
                    ,ref ValLoad);

                    foreach (MDonorPartner model in request.DonorPartners)
                    {
                        MDonorPartner DonorPartner = new MDonorPartner();

                        DonorPartner.DonorCode = model.DonorCode;
                        DonorPartner.DonorName = model.DonorName;
                        DonorPartner.FundingPartner = model.FundingPartner;
                        DonorPartner.DonorLongDescription = model.DonorLongDescription;
                        DonorPartner.DonorDescription = model.DonorDescription;

                        BDonorPartner.Insert(DonorPartner);
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
    }
}
