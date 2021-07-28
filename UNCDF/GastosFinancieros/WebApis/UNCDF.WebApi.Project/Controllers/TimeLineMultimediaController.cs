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


namespace UNCDF.WebApi.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class TimeLineMultimediaController : ControllerBase
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IWebHostEnvironment _env;
        private readonly MAwsEmail _MAwsEmail;
        private readonly MAwsS3 _MAwsS3;


        public TimeLineMultimediaController(
          IWebHostEnvironment env, IOptions<AppSettings> appSettings, IOptions<MAwsEmail> MAwsEmail, IOptions<MAwsS3> MAwsS3
         )
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsEmail = MAwsEmail.Value;
            _MAwsS3 = MAwsS3.Value;
        }


        [HttpPost]
        [Route("0/InsertTimeLineMultimedia")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public TimeLineMultimediaResponse InsertTimeLineMultimedia([FromBody] TimeLineMultimediaRequest request)
        {
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            string webRoot = _env.ContentRootPath;
            string rootPath = _appSettings.Value.rootPath;
            string TimeLineMultimediaPath = _appSettings.Value.TimeLineMultimediaPath;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            timeLineMultimedia.TimeLineId = request.TimeLineMultimedia.TimeLineId;
            timeLineMultimedia.Type = request.TimeLineMultimedia.Type;
            timeLineMultimedia.File = request.TimeLineMultimedia.File;
            timeLineMultimedia.Title = request.TimeLineMultimedia.Title;


            int Val = 0;

            timeLineMultimedia.TimeLineMulId = BTimeLineMultimedia.Insert(timeLineMultimedia, ref Val);

            if (request.TimeLineMultimedia.FileByte != null)
            {
                //Requered Update File
                string path = TimeLineMultimediaPath + "/" + timeLineMultimedia.TimeLineMulId.ToString() + request.TimeLineMultimedia.FileExt;
                timeLineMultimedia.File = path;
                BTimeLineMultimedia.Update(timeLineMultimedia, ref Val);

                byte[] File = request.TimeLineMultimedia.FileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + path;

                if (!Directory.Exists(pathAbs + rootPath + TimeLineMultimediaPath)) Directory.CreateDirectory(pathAbs + rootPath + TimeLineMultimediaPath);

                if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                System.IO.File.WriteAllBytes(pathSave, File);

                if (!BAwsSDK.UploadS3(_MAwsS3,pathSave, TimeLineMultimediaPath, timeLineMultimedia.TimeLineMulId.ToString() + request.TimeLineMultimedia.FileExt))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "TimeLine Multimedia");
                }

                System.IO.File.Delete(pathSave);
            }

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineMultimedia", timeLineMultimedia.TimeLineMulId, 1, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorInsert, "TimeLine Multimedia");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "TimeLine Multimedia");
            }

            response.TimeLineMultimedia = timeLineMultimedia;

            return response;
        }


        [HttpPost]
        [Route("0/UpdateTimeLineMultimedia")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public TimeLineMultimediaResponse UpdateTimeLineMultimedia([FromBody] TimeLineMultimediaRequest request)
        {
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            string webRoot = _env.ContentRootPath;
            string rootPath = _appSettings.Value.rootPath;
            string TimeLineMultimediaPath = _appSettings.Value.TimeLineMultimediaPath;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            timeLineMultimedia.TimeLineMulId = request.TimeLineMultimedia.TimeLineMulId;
            timeLineMultimedia.TimeLineId = request.TimeLineMultimedia.TimeLineId;
            timeLineMultimedia.Type = request.TimeLineMultimedia.Type;

            if (request.TimeLineMultimedia.FileByte != null)
            {
                timeLineMultimedia.File = TimeLineMultimediaPath + "/" + request.TimeLineMultimedia.TimeLineMulId.ToString() + request.TimeLineMultimedia.FileExt;
            }
            else
            {
                timeLineMultimedia.File = TimeLineMultimediaPath + "/" + request.TimeLineMultimedia.File;
            }

            timeLineMultimedia.Title = request.TimeLineMultimedia.Title;

            int Val = 0;

            timeLineMultimedia.TimeLineMulId = BTimeLineMultimedia.Update(timeLineMultimedia, ref Val);

            if (request.TimeLineMultimedia.FileByte != null)
            {
                byte[] File = request.TimeLineMultimedia.FileByte;

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + timeLineMultimedia.File;

                if (!Directory.Exists(pathAbs + rootPath + TimeLineMultimediaPath)) Directory.CreateDirectory(pathAbs + rootPath + TimeLineMultimediaPath);

                if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

                System.IO.File.WriteAllBytes(pathSave, File);

                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, TimeLineMultimediaPath, timeLineMultimedia.File.Replace("/", string.Empty)))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "TimeLine Multimedia");
                }

                System.IO.File.Delete(pathSave);

            }

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineMultimedia", timeLineMultimedia.TimeLineMulId, 2, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;

                if (Val.Equals(0))
                {
                    response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = Messages.Success;
                }
                else if (Val.Equals(2))
                {
                    response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                    response.Message = String.Format(Messages.ErrorUpdate, "TimeLine Multimedia");
                }
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorUpdate, "TimeLine Multimedia");
            }

            response.TimeLineMultimedia = timeLineMultimedia;

            return response;
        }

        [HttpPost]
        [Route("0/GetTimeLineMultimedia")]
        public TimeLineMultimediaResponse GetTimeLineMultimedia([FromBody] TimeLineMultimediaRequest request)
        {
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();
            string TimeLineMultimediaPath = _appSettings.Value.TimeLineMultimediaPath + "/";

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

            timeLineMultimedia.TimeLineMulId = request.TimeLineMultimedia.TimeLineMulId;

            int Val = 0;

            timeLineMultimedia = BTimeLineMultimedia.Select(timeLineMultimedia, ref Val);
            timeLineMultimedia.File = timeLineMultimedia.File.Replace(TimeLineMultimediaPath, string.Empty);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(1))
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "TimeLine Multimedia");
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "TimeLine Multimedia");
            }

            response.TimeLineMultimedia = timeLineMultimedia;

            return response;
        }


        [HttpPost]
        [Route("0/GetTimeLineMultimediaList")]
        public TimeLineMultimediasResponse GetTimeLineMultimediaList([FromBody] TimeLineMultimediaRequest request)
        {
            TimeLineMultimediasResponse response = new TimeLineMultimediasResponse();
            List<MTimeLineMultimedia> timeLineMultimedias = new List<MTimeLineMultimedia>();
            BaseRequest baseRequest = new BaseRequest();
            string TimeLineMultimediaPath = _appSettings.Value.TimeLineMultimediaPath + "/";

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();

            timeLineMultimedia.TimeLineId = request.TimeLineMultimedia.TimeLineId;
            timeLineMultimedia.Title = request.TimeLineMultimedia.Title;
            timeLineMultimedia.Type = request.TimeLineMultimedia.Type;


            int Val = 0;

            //timeLineMultimedias = BTimeLineMultimedia.Filter(timeLineMultimedia, ref Val);
            timeLineMultimedias = BTimeLineMultimedia.Filter(timeLineMultimedia, ref Val).Select(x => new MTimeLineMultimedia
            {
                TimeLineMulId = x.TimeLineMulId,
                TimeLineId = x.TimeLineId,
                Title = x.Title,
                Type = x.Type,
                File = x.File//S.Replace(TimeLineMultimediaPath,string.Empty)

            }).ToList();

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "TimeLine Multimedia");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "TimeLine Multimedia");
            }

            response.TimeLineMultimedias = timeLineMultimedias.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/DeleteTimeLineMultimedia")]
        public TimeLineMultimediaResponse DeleteTimeLineMultimedia([FromBody] TimeLineMultimediaRequest request)
        {
            TimeLineMultimediaResponse response = new TimeLineMultimediaResponse();
            MTimeLineMultimedia timeLineMultimedia = new MTimeLineMultimedia();

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

            timeLineMultimedia.TimeLineMulId = request.TimeLineMultimedia.TimeLineMulId;

            int Val = 0;

            timeLineMultimedia.TimeLineMulId = BTimeLineMultimedia.Delete(timeLineMultimedia, ref Val);

            if (Val.Equals(0))
            {
                //Record the audit
                Val = BAudit.RecordAudit("TimeLineMultimedia", timeLineMultimedia.TimeLineMulId, 3, baseRequest.Session.UserId);

                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "TimeLine Multimedia");
            }

            response.TimeLineMultimedia = timeLineMultimedia;

            return response;
        }


    }
}