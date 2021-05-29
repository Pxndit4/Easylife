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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class ProjectController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;

        public ProjectController(IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3)
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }


        [HttpPost]
        [Route("0/ProjectProperties")]
        public ProjectPropertiesResponse ProjectProperties([FromBody] BaseRequest request)
        {
            ProjectPropertiesResponse response = new ProjectPropertiesResponse();

            List<MProgramName> programNames = new List<MProgramName>();
            List<MDonorPartner> donorPartners = new List<MDonorPartner>();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                programNames = BProgramName.List();
                donorPartners = BDonorPartner.List();

                response.ProgramNames = programNames.ToArray();
                response.DonorPartners = donorPartners.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Route("0/InsertProject")]
        public ProjectResponse InsertProject([FromBody] ProjectRequest request)
        {
            ProjectResponse response = new ProjectResponse();

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

                MProject project = new MProject();

                project.ProjectCode = request.Project.ProjectCode;
                project.Description = request.Project.Description;
                project.Type = request.Project.Type;
                project.Status = request.Project.Status;
                project.StartDate = request.Project.StartDate;
                project.EndDate = request.Project.EndDate;
                project.Title = request.Project.Title;
                project.AwardId = request.Project.AwardId;
                project.AwardStatus = request.Project.AwardStatus;
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
