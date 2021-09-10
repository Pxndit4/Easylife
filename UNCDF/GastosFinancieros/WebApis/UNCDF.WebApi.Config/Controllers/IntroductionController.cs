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

namespace UNCDF.WebApi.Config.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class IntroductionController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsS3 _MAwsS3;

        public IntroductionController(
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
        [Route("0/GetIntroduccion")]
        public IntroductionResponse GetIntroduccion([FromBody] IntroductionRequest request)
        {
            IntroductionResponse response = new IntroductionResponse();
            List<MIntroduction> introductions = new List<MIntroduction>();
            MIntroduction MIntroduction = new MIntroduction();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            MIntroduction.IntroductionId = 1;
            MIntroduction.Title = request.Introduction.Title;
            MIntroduction.Description = request.Introduction.Description;
            MIntroduction.Status = request.Introduction.Status;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            introductions = BIntroduction.List(MIntroduction, baseRequest);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.Introductions = introductions.ToArray();

            return response;
        }


        [HttpPost]
        [Route("0/InsertIntroduction")]
        public IntroductionBEResponse InsertIntroduction([FromBody] IntroductionRequest request)
        {
            IntroductionBEResponse response = new IntroductionBEResponse();
            MIntroduction introduction = new MIntroduction();

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
            string IntroductionPath = _appSettings.Value.IntroductionPath;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            introduction.Title = request.Introduction.Title;
            introduction.Description = request.Introduction.Description;
            introduction.Image = request.Introduction.Image;
            introduction.Order = request.Introduction.Order;
            introduction.Status = request.Introduction.Status;

            int Val = 0;

            introduction.IntroductionId = BIntroduction.Insert(introduction, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Introduction", introduction.IntroductionId, 1, baseRequest.Session.UserId);

            //Requered Update File
            string path = IntroductionPath + "/" + introduction.IntroductionId.ToString() + request.Introduction.ImageExtension;
            introduction.Image = path;

            BIntroduction.Update(introduction, ref Val);

            byte[] File = request.Introduction.FileByte;

            Uri webRootUri = new Uri(webRoot);
            string pathAbs = webRootUri.AbsolutePath;
            var pathSave = pathAbs + rootPath + path;

            if (!Directory.Exists(pathAbs + rootPath + IntroductionPath)) Directory.CreateDirectory(pathAbs + rootPath + IntroductionPath);

            if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

            System.IO.File.WriteAllBytes(pathSave, File);
            if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, IntroductionPath, introduction.IntroductionId.ToString() + request.Introduction.ImageExtension))
            //if (!BAplication.AWS_Upload_S3(pathSave, "unitlifebucket", IntroductionPath, introduction.IntroductionId.ToString() + request.Introduction.ImageExtension))
            {
                response.Message = String.Format(Messages.ErrorLoadPhoto, "Introduction");
            }


            System.IO.File.Delete(pathSave);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorInsert, "Introduction");
            }

            response.Introduction = introduction;

            return response;
        }


        [HttpPost]
        [Route("0/UpdateIntroduction")]
        public IntroductionBEResponse UpdateIntroduction([FromBody] IntroductionRequest request)
        {
            IntroductionBEResponse response = new IntroductionBEResponse();
            MIntroduction ent = new MIntroduction();

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
            string IntroductionPath = _appSettings.Value.IntroductionPath + "/";

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            ent.IntroductionId = request.Introduction.IntroductionId;
            ent.Title = request.Introduction.Title;
            ent.Description = request.Introduction.Description;
            ent.Order = request.Introduction.Order;
            ent.Image = IntroductionPath + ((request.Introduction.FileByte != null) ? request.Introduction.IntroductionId.ToString() + request.Introduction.ImageExtension : request.Introduction.Image);
            ent.Status = request.Introduction.Status;

            int Val = 0;

            ent.IntroductionId = BIntroduction.Update(ent, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Introduction", ent.IntroductionId, 2, baseRequest.Session.UserId);

            if (request.Introduction.FileByte != null)
            {

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + ent.Image;

                byte[] File = request.Introduction.FileByte;

                if (!Directory.Exists(pathAbs + rootPath + IntroductionPath))
                {
                    Directory.CreateDirectory(pathAbs + rootPath + IntroductionPath);
                }

                if (System.IO.File.Exists(pathSave))
                {
                    System.IO.File.Delete(pathSave);
                }

                System.IO.File.WriteAllBytes(pathSave, File);

                string ImageFileName = ent.IntroductionId.ToString() + request.Introduction.ImageExtension;
                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, IntroductionPath.Replace("/", string.Empty), ImageFileName))
                    //if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, IntroductionPath, ent.IntroductionId.ToString() + request.Introduction.ImageExtension))
                //if (!BAplication.AWS_Upload_S3(pathSave, "unitlifebucket", IntroductionPath.Replace("/", string.Empty), ImageFileName))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "Introduction");
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
                response.Message = String.Format(Messages.ErrorUpdate, "Introduction");
            }

            response.Introduction = ent;

            return response;
        }

        [HttpPost]
        [Route("0/GetIntroduction")]
        public IntroductionBEResponse GetIntroduction([FromBody] IntroductionRequest request)
        {
            IntroductionBEResponse response = new IntroductionBEResponse();
            MIntroduction ent = new MIntroduction();
            string IntroductionPath = _appSettings.Value.IntroductionPath + "/";

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

            ent.IntroductionId = request.Introduction.IntroductionId;

            int Val = 0;

            ent = BIntroduction.Select(ent, ref Val);
            ent.Image = ent.Image.Replace(IntroductionPath, string.Empty);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(1))
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Introduction");
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Introduction");
            }

            response.Introduction = ent;

            return response;
        }


        [HttpPost]
        [Route("0/FilterIntroductions")]
        public IntroductionsResponse FilterIntroductions([FromBody] IntroductionRequest request)
        {
            IntroductionsResponse response = new IntroductionsResponse();
            List<MIntroduction> ents = new List<MIntroduction>();
            BaseRequest baseRequest = new BaseRequest();

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

            MIntroduction ent = new MIntroduction();

            ent.Title = request.Introduction.Title;
            ent.Description = request.Introduction.Description;
            ent.Status = request.Introduction.Status;

            int Val = 0;

            ents = BIntroduction.List(ent, baseRequest);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Introductions");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Introductions");
            }

            response.Introductions = ents.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/DeleteIntroduction")]
        public IntroductionBEResponse DeleteIntroduction([FromBody] IntroductionRequest request)
        {
            IntroductionBEResponse response = new IntroductionBEResponse();
            MIntroduction ent = new MIntroduction();

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

            ent.IntroductionId = request.Introduction.IntroductionId;


            int Val = 0;

            ent.IntroductionId = BIntroduction.Delete(ent, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Introduction", ent.IntroductionId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Introduction");
            }

            response.Introduction = ent;

            return response;
        }


    }
}
