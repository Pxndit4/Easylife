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
using System.IO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UNCDF.WebApi.Project.Crontrollers
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

                response.Code = "0";
                response.Message = "Success";
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
        [Route("0/ProjectFinancialProperties")]
        public ProjectFinancialPropertiesResponse ProjectFinancialProperties([FromBody] BaseRequest request)
        {
            ProjectFinancialPropertiesResponse response = new ProjectFinancialPropertiesResponse();

            List<MDeparment> deparments = new List<MDeparment>();
            List<MFund> funds = new List<MFund>();
            List<MImplementAgency> implementAgencies = new List<MImplementAgency>();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                deparments = BDeparment.List();
                funds = BFund.List();
                implementAgencies = BImplementAgency.List();

                response.Code = "0";
                response.Message = "Success";
                response.Deparments = deparments.ToArray();
                response.Funds = funds.ToArray();
                response.ImplementAgencies = implementAgencies.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Route("0/GetProjects")]
        public ProjectsResponse GetProjects([FromBody] ProjectRequest request)
        {
            ProjectsResponse response = new ProjectsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                MProject project = new MProject();
                project.EffectiveStatus = request.Project.EffectiveStatus;
                project.ProjectCode = request.Project.ProjectCode;
                project.Title = request.Project.Title;
                project.StartDate = request.Project.StartDate;
                project.EndDate = request.Project.EndDate;

                List<MProject> projects = BProject.List(project);

                response.Code = "0";
                response.Message = "Success";
                response.Projects = projects.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }


        [HttpPost]
        [Route("0/UpdateProject")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public ProjectResponse UpdateProject([FromBody] ProjectRequest request)
        {
            ProjectResponse response = new ProjectResponse();
            string webRoot = _env.ContentRootPath;
            string rootPath = _appSettings.Value.rootPath;
            string ProjectPath = _appSettings.Value.ProjectPath;
            //string accesskey = _appSettings.Value.AccessKey;
            //string secretKey = _appSettings.Value.SecretKey;
            //string bucketName = _appSettings.Value.BucketName;


            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            string Guid = BAplication.GenerateGuid();

            MProject ProjectBE = new MProject();
            ProjectBE.ProjectId = request.Project.ProjectId;
            ProjectBE.Image = ProjectPath + "/" + ((request.Project.FileByte != null) ? request.Project.ProjectId.ToString() + Guid + request.Project.Ext : request.Project.Image);
            ProjectBE.Video = ProjectPath + "/" + ((request.Project.VideoFileByte != null) ? request.Project.ProjectId.ToString() + Guid + request.Project.ExtVideo : request.Project.Video);
            ProjectBE.IsVisible = request.Project.IsVisible;
            ProjectBE.Donation = request.Project.Donation;
            int Val = 0;

            ProjectBE.ProjectId = BProject.Update(ProjectBE);


            if (request.Project.FileByte != null)
            {
                byte[] File = request.Project.FileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + ProjectBE.Image;

                if (!Directory.Exists(pathAbs + rootPath + ProjectPath)) Directory.CreateDirectory(pathAbs + rootPath + ProjectPath);

                if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                System.IO.File.WriteAllBytes(pathSave, File);

                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, ProjectPath, request.Project.ProjectId.ToString() + Guid  + request.Project.Ext))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "Project");
                }

                System.IO.File.Delete(pathSave);

            }

            if (request.Project.VideoFileByte != null)
            {
                byte[] VideoFile = request.Project.VideoFileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + ProjectBE.Video;

                if (!Directory.Exists(pathAbs + rootPath + ProjectPath)) Directory.CreateDirectory(pathAbs + rootPath + ProjectPath);

                if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                System.IO.File.WriteAllBytes(pathSave, VideoFile);

                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, ProjectPath, request.Project.ProjectId.ToString() + Guid + request.Project.ExtVideo))
                {
                    response.Message = String.Format(Messages.ErrorLoadVideo, "Project");
                }

                System.IO.File.Delete(pathSave);

            }

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "Banner");
            }

            response.Project = ProjectBE;

            return response;
        }

        [HttpPost]
        [Route("0/GetProject")]
        public ProjectResponse GetProject([FromBody] ProjectRequest request)
        {
            ProjectResponse response = new ProjectResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message =Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MProject project = new MProject();
            BaseRequest baseRequest = new BaseRequest();

            project.ProjectId = request.Project.ProjectId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            project = BProject.Get(project);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Project");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Project");
            }

            response.Project = project;

            return response;
        }


        [HttpPost]
        [Route("0/InsertProject")]
        public ProjectResponse InsertProject([FromBody] ProjectsRequest request)
        {
            ProjectResponse response = new ProjectResponse();

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

                    foreach (MProject model in request.Projects)
                    {
                        MProject project = new MProject();

                        project.Department = model.Department;
                        project.ProjectCode = model.ProjectCode;
                        project.Description = model.Description;
                        project.Type = model.Type;
                        project.EffectiveStatus = model.EffectiveStatus;
                        project.StatusEffDate = model.StatusEffDate;
                        project.StatusEffSeq = model.StatusEffSeq;
                        project.Status = model.Status;
                        project.StatusDescription = model.StatusDescription;
                        project.StartDate = model.StartDate;
                        project.EndDate = model.EndDate;
                        project.Title = model.Title;
                        project.AwardId = model.AwardId;
                        project.AwardStatus = model.AwardStatus;

                        BProject.Insert(project);
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
        [Route("0/GetProjectsScroll")]
        public ProjectsResponse GetProjectsScroll([FromBody] ProjectRequest request)
        {
            ProjectsResponse response = new ProjectsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                List<MProject> projects = BProject.ListScroll();

                response.Code = "0";
                response.Message = "Success";
                response.Projects = projects.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }

        [HttpPost]
        [Route("0/GetProjectsFilter")]
        public ProjectsResponse GetProjectsFilter([FromBody] ProjectRequest request)
        {
            ProjectsResponse response = new ProjectsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                MProject ent = new MProject();
                ent.Continents = request.Project.Continents;
                ent.Countries = request.Project.Countries;
                ent.Title = request.Project.Title;
                ent.Anio = request.Project.Anio;

                List<MProject> projects = BProject.ListFilter(ent);

                response.Code = "0";
                response.Message = "Success";
                response.Projects = projects.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2";
                response.Message = ex.Message;
            }

            return response;

        }



        [HttpPost]
        [Route("0/GetProjectsCodeExclusions")]
        public ProjectsResponse GetProjectsCodeExclusions([FromBody] ProjectRequest request)
        {
            ProjectsResponse response = new ProjectsResponse();

            try
            {
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }

                MProject project = new MProject();
                project.EffectiveStatus = request.Project.EffectiveStatus;
                project.ProjectCode = request.Project.ProjectCode;
                project.Title = request.Project.Title;
                project.StartDate = request.Project.StartDate;
                project.EndDate = request.Project.EndDate;

                List<MProject> projects = BProject.ListProjectCodeExclusions(project);

                response.Code = "0";
                response.Message = "Success";
                response.Projects = projects.ToArray();
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
