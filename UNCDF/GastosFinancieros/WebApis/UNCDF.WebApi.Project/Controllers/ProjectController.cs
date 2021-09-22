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

                BaseRequest baseRequest = new BaseRequest();
                baseRequest.Session = request.Session;

                List<MProject> projects = BProject.List(project,baseRequest.Session);

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

            MProject MProject = new MProject();
            MProject.ProjectId = request.Project.ProjectId;
            MProject.Image = ProjectPath + "/" + ((request.Project.FileByte != null) ? request.Project.ProjectId.ToString() + Guid + request.Project.Ext : request.Project.Image);
            MProject.Video = ProjectPath + "/" + ((request.Project.VideoFileByte != null) ? request.Project.ProjectId.ToString() + Guid + request.Project.ExtVideo : request.Project.Video);
            MProject.IsVisible = request.Project.IsVisible;
            MProject.Donation = request.Project.Donation;
            int Val = 0;

            MProject.ProjectId = BProject.Update(MProject);


            if (request.Project.FileByte != null)
            {
                byte[] File = request.Project.FileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + MProject.Image;

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
                var pathSave = pathAbs + rootPath + MProject.Video;

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

            response.Project = MProject;

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
        [Route("0/GetProjectsGroupByCountry")]
        public ProjectsResponse GetProjectsGroupByCountry([FromBody] ProjectRequest request)
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

                List<MProject> projects = BProject.ListGroupbyCountry(ent);

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
        [Route("0/GetProjectsByCountry")]
        public ProjectsResponse GetProjectsByCountry([FromBody] ProjectRequest request)
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
                ent.CountryId = request.Project.CountryId;

                List<MProject> projects = BProject.ListbyCountry(ent);

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

        [HttpPost]
        [Route("0/GetTimeLines")]
        public TimeLinesResponse GetTimeLines([FromBody] TimeLineRequest request)
        {
            TimeLinesResponse response = new TimeLinesResponse();
            List<MTimeLine> timeLines = new List<MTimeLine>();
            MProject MProject = new MProject();
            BaseRequest baseRequest = new BaseRequest();
            string Error = string.Empty;

            try
            {
                /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
                if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
                {
                    response.Code = "2";
                    response.Message = Messages.ApplicationTokenNoAutorize;
                    return response;
                }
                /*************FIN DEL METODO*************/

                MProject.ProjectId = request.TimeLine.ProjectId;

                baseRequest.Language = request.Language;
                baseRequest.Session = request.Session;

                int Val = 0;                

                timeLines = BTimeLine.List(MProject, baseRequest, ref Val, ref Error);


                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLines");
                }
                else
                {
                    response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.NotReults, "TimeLines");
                }

                response.TimeLines = timeLines.ToArray();
            }
            catch (Exception ex)
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = ex.Message + "|" + ex.StackTrace + "|" + Error;
            }   

            return response;
        }

        [HttpPost]
        [Route("0/GetTimeLine")]
        public TimeLineResponse GetTimeLine([FromBody] TimeLineRequest request)
        {
            TimeLineResponse response = new TimeLineResponse();
            MTimeLine timeLine = new MTimeLine();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            timeLine.TimeLineId = request.TimeLine.TimeLineId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            timeLine = BTimeLine.Sel(timeLine, baseRequest, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "TimeLine");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "TimeLine");
            }

            response.TimeLine = timeLine;

            return response;
        }

        [HttpPost]
        [Route("0/GetTimeLineMultimedia")]
        public TimeLineMultimediasResponse GetTimeLineMultimedia([FromBody] TimeLineRequest request)
        {
            TimeLineMultimediasResponse response = new TimeLineMultimediasResponse();
            List<MTimeLineMultimedia> timeLines = new List<MTimeLineMultimedia>();
            MTimeLine timeLine = new MTimeLine();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            timeLine.TimeLineId = request.TimeLine.TimeLineId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            timeLines = BTimeLineMultimedia.List(timeLine, baseRequest, ref Val);

            if (timeLines.Count.Equals(0))
            {
                MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();

                timeLineMultimedia.TimeLineId = request.TimeLine.TimeLineId;
                timeLineMultimedia.Title = "Image";
                timeLineMultimedia.Type = 1;
                timeLineMultimedia.File = Path.Combine(Constant.S3Server, "project/timeline/multimedia") + "/without_image.png";
                timeLines.Add(timeLineMultimedia);

                timeLineMultimedia = new MTimeLineMultimedia();
                timeLineMultimedia.TimeLineId = request.TimeLine.TimeLineId;
                timeLineMultimedia.Title = "Video";
                timeLineMultimedia.Type = 2;
                timeLineMultimedia.File = Path.Combine(Constant.S3Server, "project/timeline/multimedia") + "/WithoutVideo.mp4";
                timeLines.Add(timeLineMultimedia);
            }
            else
            {
                MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();
                timeLineMultimedia = timeLines.Find(x => x.Type.Equals(1));

                if (timeLineMultimedia == null)
                {
                    timeLineMultimedia = new MTimeLineMultimedia();
                    timeLineMultimedia.TimeLineId = request.TimeLine.TimeLineId;
                    timeLineMultimedia.Title = "Image";
                    timeLineMultimedia.Type = 1;
                    timeLineMultimedia.File = Path.Combine(Constant.S3Server, "project/timeline/multimedia") + "/without_image.png";
                    timeLines.Add(timeLineMultimedia);
                }

                timeLineMultimedia = new MTimeLineMultimedia();
                timeLineMultimedia = timeLines.Find(x => x.Type.Equals(2));

                if (timeLineMultimedia == null)
                {
                    timeLineMultimedia = new MTimeLineMultimedia();
                    timeLineMultimedia.TimeLineId = request.TimeLine.TimeLineId;
                    timeLineMultimedia.Title = "Video";
                    timeLineMultimedia.Type = 2;
                    timeLineMultimedia.File = Path.Combine(Constant.S3Server, "project/timeline/multimedia") + "/WithoutVideo.mp4";
                    timeLines.Add(timeLineMultimedia);
                }
            }

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLine Multimedias");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLine Multimedias");
            }

            response.TimeLineMultimedias = timeLines.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetTestimonials")]
        public TimeLineTestimonialsResponse GetTestimonials([FromBody] TimeLineTestimonialRequest request)
        {
            TimeLineTestimonialsResponse response = new TimeLineTestimonialsResponse();
            List<MTimeLineTestimonial> testimonials = new List<MTimeLineTestimonial>();
            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MTimeLineTestimonial MTimeLineTestimonial = new MTimeLineTestimonial();
            MTimeLineTestimonial.TimeLineId = request.Testimonial.TimeLineId;
            MTimeLineTestimonial.Name = request.Testimonial.Name;

            int Val = 0;

            testimonials = BTimeLineTestimonial.List(MTimeLineTestimonial, baseRequest, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Testimonials");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Testimonials");
            }

            response.Testimonials = testimonials.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetRandomProjects")]
        public ProjectsResponse GetRandomProjects([FromBody] ProjectRequest request)
        {
            ProjectsResponse response = new ProjectsResponse();
            List<MProject> projects = new List<MProject>();
            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            int Val = 0;

            projects = BProject.RandomLis(baseRequest, ref Val);


            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Projects");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Projects");
            }

            response.Projects = projects.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetProjectDetails")]
        public ProjectResponse GetProjectDetails([FromBody] ProjectRequest request)
        {
            ProjectResponse response = new ProjectResponse();
            MProject timeLine = new MProject();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            timeLine.ProjectId = request.Project.ProjectId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            timeLine = BProject.GetDetails(timeLine, ref Val);


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

            response.Project = timeLine;

            return response;
        }

        [HttpPost]
        [Route("0/GetProjectsFlags")]
        public ProjectsFlagsResponse GetProjectsFlags([FromBody] ProjectRequest request)
        {
            ProjectsFlagsResponse response = new ProjectsFlagsResponse();

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

            int Val = 0;
            string[] Flags;

            Flags = BProject.GetFlags(ref Val).ToArray();

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Flags");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Flags");
            }

            response.Flags = Flags;

            return response;
        }

        [HttpPost]
        [Route("0/GetProjectFinancialsYears")]
        public ProjectsFinancialYearResponse GetProjectFinancialsYears([FromBody] ProjectFinancialRequest request)
        {
            ProjectsFinancialYearResponse response = new ProjectsFinancialYearResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MProjectFinancials projectFinancial = new MProjectFinancials();
            BaseRequest baseRequest = new BaseRequest();

            projectFinancial.ProjectId = request.ProjectFinancial.ProjectId;
            projectFinancial.Year = request.ProjectFinancial.Year;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;
            string[] Years;

            Years = BProject.YearLis(projectFinancial, ref Val).ToArray();

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Years");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Years");
            }

            response.Years = Years;

            return response;
        }

        [HttpPost]
        [Route("0/GetProjectFinancialsByYears")]
        public ProjectsFinancialResponse GetProjectFinancialsByYears([FromBody] ProjectFinancialRequest request)
        {
            ProjectsFinancialResponse response = new ProjectsFinancialResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MProjectFinancials projectFinancial = new MProjectFinancials();
            BaseRequest baseRequest = new BaseRequest();

            projectFinancial.ProjectId = request.ProjectFinancial.ProjectId;
            projectFinancial.Year = request.ProjectFinancial.Year;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;


            response.Financial = BProject.GetFinancialsByYear(projectFinancial, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Financials");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Financials");
            }
            
            return response;
        }

        [HttpPost]
        [Route("0/GetProjectFinancials")]
        public ProjectsFinancialResponse GetProjectFinancials([FromBody] ProjectFinancialRequest request)
        {
            ProjectsFinancialResponse response = new ProjectsFinancialResponse();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MProjectFinancials projectFinancial = new MProjectFinancials();
            BaseRequest baseRequest = new BaseRequest();

            projectFinancial.ProjectId = request.ProjectFinancial.ProjectId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;


            response.Financial = BProject.GetFinancials(projectFinancial, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Financials");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Financials");
            }

            return response;
        }
    }
}
