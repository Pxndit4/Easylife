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
    public class ProgramNameController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public ProgramNameController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }

        [HttpPost]
        [Route("0/GetProgramNames")]
        public ProgramNamesResponse GetProgramNames([FromBody] BaseRequest request)
        {
            ProgramNamesResponse response = new ProgramNamesResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MProgramName> programNames = BProgramName.List();

                response.ProgramNames = programNames.ToArray();
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
        [Route("0/GetValidProgramNames")]
        public ProgramNamesResponse GetValidProgramNames([FromBody] BaseRequest request)
        {
            ProgramNamesResponse response = new ProgramNamesResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MProgramName> programNames = BProgramName.ListValidProgramName();

                response.ProgramNames = programNames.ToArray();
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
        [Route("0/InsertProgramName")]
        public BaseResponse InsertProgramName([FromBody] ProgramNamesRequest request)
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

                    foreach (MProgramName model in request.ProgramNames)
                    {
                        MProgramName programName = new MProgramName();

                        programName.ProjectCode = model.ProjectCode;
                        programName.ProgramName = model.ProgramName;
                        programName.DonorCode = model.DonorCode;
                        programName.ProjectDetails = model.ProjectDetails;
                        programName.Sector = model.Sector;
                        programName.TaskManager = model.TaskManager;
                        programName.SDG = model.SDG;

                        BProgramName.Insert(programName);

                        //if (!programName.SDG.Equals(""))
                        //{
                        //    BProgramName.InsertSDG(programName);
                        //}
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
    }
}
