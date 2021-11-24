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

namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class ProjectFinancialController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public ProjectFinancialController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetProjectFinancials")]
        public ProjectFinancialResponse GetProjectFinancials([FromBody] ProjectFinancialRequest request)
        {
            ProjectFinancialResponse response = new ProjectFinancialResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                int Val = 0;

                MProjectFinancials ent = new MProjectFinancials();
                ent.Year = request.ProjectFinancial.Year;
                ent.ProjectCode = request.ProjectFinancial.ProjectCode;


                List<MProjectFinancials> ProjectFinancials = BProjectFinancial.FilProjectFinancial(ent,ref Val);

                response.ProjectFinancials = ProjectFinancials.ToArray();
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
        [Route("0/InsertProjectFinancial")]
        public BaseResponse InsertProjectFinancial([FromBody] ProjectFinancialsRequest request)
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

                    BaseRequest baseRequest = new BaseRequest();

                    //validar años a cargar
                    if (request.ProjectFinancials.Count() == 0)
                    {
                        response.Code = "2";
                        response.Message = "There are no records to process";
                        return response;
                    }


                    foreach (MProjectFinancials model in request.ProjectFinancials)
                    {
                        MProjectFinancials ProjectFinancial = new MProjectFinancials();
                        ProjectFinancial.ProjectId = model.ProjectId;
                        ProjectFinancial.Year = model.Year;
                        ProjectFinancial.OperUnit = model.OperUnit;
                        ProjectFinancial.DeparmentCode = model.DeparmentCode;
                        ProjectFinancial.ProjectCode = model.ProjectCode;
                        ProjectFinancial.DescrProject = model.DescrProject;
                        ProjectFinancial.ProjectManager = model.ProjectManager;
                        ProjectFinancial.ImplementAgencyCode = model.ImplementAgencyCode;
                        ProjectFinancial.ShortDesc = model.ShortDesc;
                        ProjectFinancial.FundCode = model.FundCode;
                        ProjectFinancial.DescrFund = model.DescrFund;
                        ProjectFinancial.Budget = model.Budget;
                        ProjectFinancial.PreEncumbrance = model.PreEncumbrance;
                        ProjectFinancial.Encumbrance = model.Encumbrance;
                        ProjectFinancial.Disbursement = model.Disbursement;
                        ProjectFinancial.Expenditure = model.Expenditure;
                        ProjectFinancial.Balance = model.Balance;
                        ProjectFinancial.Spent = model.Spent;
                        BProjectFinancial.Insert(ProjectFinancial);
                    }

                    response.Code = "0";
                    response.Message = "Success";

                    scope.Complete();
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
        [Route("0/InsertProjectFinancialHistory")]
        public BaseResponse InsertProjectFinancialHistory([FromBody] ProjectFinancialsRequest request)
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

                    BaseRequest baseRequest = new BaseRequest();
                    foreach (MProjectFinancials model in request.ProjectFinancials)
                    {
                        MProjectFinancials ProjectFinancial = new MProjectFinancials();
                        ProjectFinancial.ProjectId = model.ProjectId;
                        ProjectFinancial.Year = model.Year;
                        ProjectFinancial.OperUnit = model.OperUnit;
                        ProjectFinancial.DeparmentCode = model.DeparmentCode;
                        ProjectFinancial.ProjectCode = model.ProjectCode;
                        ProjectFinancial.DescrProject = model.DescrProject;
                        ProjectFinancial.ProjectManager = model.ProjectManager;
                        ProjectFinancial.ImplementAgencyCode = model.ImplementAgencyCode;
                        ProjectFinancial.ShortDesc = model.ShortDesc;
                        ProjectFinancial.FundCode = model.FundCode;
                        ProjectFinancial.DescrFund = model.DescrFund;
                        ProjectFinancial.Budget = model.Budget;
                        ProjectFinancial.PreEncumbrance = model.PreEncumbrance;
                        ProjectFinancial.Encumbrance = model.Encumbrance;
                        ProjectFinancial.Disbursement = model.Disbursement;
                        ProjectFinancial.Expenditure = model.Expenditure;
                        ProjectFinancial.Balance = model.Balance;
                        ProjectFinancial.Spent = model.Spent;
                        BProjectFinancial.InsertHistory(ProjectFinancial);
                    }

                    response.Code = "0";
                    response.Message = "Success";

                    scope.Complete();
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
        [Route("0/GetProjectFinancialValidYear")]
        public ProjectFinancialResponse GetProjectFinancialValidYear([FromBody] ProjectFinancialYearRequest request)
        {
            ProjectFinancialResponse response = new ProjectFinancialResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                int Val = 0;


                List<MProjectFinancials> ProjectFinancials = BProjectFinancial.ProjectFinancial_ValidYear(request.Year, ref Val);

                response.ProjectFinancials = ProjectFinancials.ToArray();
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

    }
}
