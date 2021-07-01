using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;
using UNCDF.Utilities;

namespace UNCDF.WebApi.Config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_corsPolicy")]
    public class LanguageController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsS3 _MAwsS3;

        public LanguageController(
            IHostingEnvironment env,
            IOptions<AppSettings> appSettings,
            IOptions<MAwsS3> MAwsS3
        )
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsS3 = MAwsS3.Value;
        }


        // POST api/values
        [HttpPost]
        [Route("0/GetLanguages")]
        public LanguagesResponse GetLanguages([FromBody] LanguageRequest request)
        {
            LanguagesResponse response = new LanguagesResponse();
            List<MLanguage> languages = new List<MLanguage>();
            string LanguagePath = _appSettings.Value.LanguagePath + "/";

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

            int Val = 0;

            languages = BLanguage.Lis(ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Languages");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Languages");
            }

            response.Languages = languages.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/GetLanguage")]
        public LanguageResponse GetLanguage([FromBody] LanguageRequest request)
        {
            LanguageResponse response = new LanguageResponse();
            MLanguage language = new MLanguage();
            string LanguagePath = _appSettings.Value.LanguagePath + "/";

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

            int Val = 0;

            language.LanguageId = request.MLanguage.LanguageId;

            //language = BLanguage.Select(language, ref Val);
            language = BLanguage.Select(language, ref Val);
            language.Flag = language.Flag.Replace(LanguagePath, string.Empty);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Language");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Language");
            }

            response.Language = language;

            return response;
        }

        [HttpPost]
        [Route("0/InsertLanguage")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public LanguageResponse InsertLanguage([FromBody] LanguageRequest request)
        {
            LanguageResponse response = new LanguageResponse();
            MLanguage language = new MLanguage();

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
            string LanguagePath = _appSettings.Value.LanguagePath;

            int Val = 0;

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            language.Code = request.MLanguage.Code;
            language.Description = request.MLanguage.Description;
            language.Flag = request.MLanguage.Flag;
            language.Status = request.MLanguage.Status;


            language.LanguageId = BLanguage.Insert(language, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Language", language.LanguageId, 1, baseRequest.Session.UserId);

            //Requered Update File
            string path = LanguagePath + "/" + language.LanguageId.ToString() + request.MLanguage.FlagExtension;
            language.Flag = path;
            BLanguage.Update(language, ref Val);

            byte[] File = request.MLanguage.FileByte;

            Uri webRootUri = new Uri(webRoot);
            string pathAbs = webRootUri.AbsolutePath;
            var pathSave = pathAbs + rootPath + path;

            if (!Directory.Exists(pathAbs + rootPath + LanguagePath)) Directory.CreateDirectory(pathAbs + rootPath + LanguagePath);

            if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

            System.IO.File.WriteAllBytes(pathSave, File);

            if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, LanguagePath, language.LanguageId.ToString() + request.MLanguage.FlagExtension))
            {
                response.Message = String.Format(Messages.ErrorLoadPhoto, "Language");
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
                response.Message = String.Format(Messages.ErrorInsert, "Language");
            }

            response.Language = language;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateLanguage")]
        public LanguageResponse UpdateLanguage([FromBody] LanguageRequest request)
        {
            LanguageResponse response = new LanguageResponse();
            MLanguage language = new MLanguage();

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
            string LanguagePath = _appSettings.Value.LanguagePath + "/";

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            int Val = 0;

            language.LanguageId = request.MLanguage.LanguageId;
            language.Code = request.MLanguage.Code;
            language.Description = request.MLanguage.Description;
            language.Flag = LanguagePath + ((request.MLanguage.FileByte != null) ? request.MLanguage.LanguageId.ToString() + request.MLanguage.FlagExtension : request.MLanguage.Flag);
            //language.Flag = LanguagePath + request.LanguageBE.Flag;
            language.Status = request.MLanguage.Status;

            //language.FlagOld = LanguagePath + request.LanguageBE.FlagOld;

            language.LanguageId = BLanguage.Update(language, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Language", language.LanguageId, 2, baseRequest.Session.UserId);

            if (request.MLanguage.FileByte != null)
            {

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + language.Flag;

                byte[] File = request.MLanguage.FileByte;

                if (!Directory.Exists(pathAbs + rootPath + LanguagePath))
                {
                    Directory.CreateDirectory(pathAbs + rootPath + LanguagePath);
                }

                if (System.IO.File.Exists(pathSave))
                {
                    System.IO.File.Delete(pathSave);
                }

                System.IO.File.WriteAllBytes(pathSave, File);

                string ImageFileName = language.LanguageId.ToString() + request.MLanguage.FlagExtension;

                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave,  LanguagePath.Replace("/", string.Empty), ImageFileName))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "Language");
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
                response.Message = String.Format(Messages.ErrorUpdate, "Language");
            }

            response.Language = language;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteLanguage")]
        public LanguageResponse DeleteLanguage([FromBody] LanguageRequest request)
        {
            LanguageResponse response = new LanguageResponse();
            MLanguage language = new MLanguage();

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

            int Val = 0;

            language.LanguageId = request.MLanguage.LanguageId;

            language.LanguageId = BLanguage.Delete(language, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Language", language.LanguageId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Language");
            }

            response.Language = language;

            return response;
        }

        [HttpPost]
        [Route("0/FilterLanguages")]
        public LanguagesResponse FilterLanguages([FromBody] LanguageRequest request)
        {
            LanguagesResponse response = new LanguagesResponse();
            MLanguage language = new MLanguage();
            List<MLanguage> languages = new List<MLanguage>();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            //baseRequest.Language = request.Language;
            //baseRequest.Session = request.Session;

            int Val = 0;

            language.Description = request.MLanguage.Description;
            language.Status = request.MLanguage.Status;


            languages = BLanguage.Filter(language, ref Val).Select(x => new MLanguage
            {
                LanguageId = x.LanguageId,
                Description = x.Description,
                Flag = x.Flag.Replace(_appSettings.Value.LanguagePath, string.Empty),
                Code = x.Code,
                Status = x.Status
            }).ToList();

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Languages");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Languages");
            }

            response.Languages = languages.ToArray();

            return response;
        }
    }
}
