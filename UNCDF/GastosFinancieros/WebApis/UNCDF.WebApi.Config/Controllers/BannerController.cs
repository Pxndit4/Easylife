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
    public class BannerController : ControllerBase
    {

        private readonly IHostingEnvironment _env;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly MAwsS3 _MAwsS3;

        public BannerController(
            IHostingEnvironment env,
            IOptions<AppSettings> appSettings,
            IOptions<MAwsS3> MAwsS3
        )
        {
            _env = env;
            _appSettings = appSettings;
            _MAwsS3 = MAwsS3.Value;
        }



        /// <summary>
        /// Metodo para obtener los banner del Home de la WebSite
        /// </summary>     
        /// <returns></returns>
        [HttpPost]
        [Route("0/GetBanners")]
        public BannersResponse GetBanners([FromBody] BannerRequest request)
        {
            BannersResponse response = new BannersResponse();
            List<MBanner> bannersBE = new List<MBanner>();

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

            bannersBE = BBanner.RandomLis(baseRequest, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Banners");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Banners");
            }

            response.banners = bannersBE.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/InsertBanner")]
        public BannerResponse InsertBanner([FromBody] BannerRequest request)
        {
            BannerResponse response = new BannerResponse();
            MBanner banner = new MBanner();
            //file 
            string webRoot = _env.ContentRootPath;
            string rootPath = _appSettings.Value.rootPath;
            string BannerPath = _appSettings.Value.BannerPath;

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

            banner.Title = request.banner.Title;
            banner.SubTile = request.banner.SubTile;
            banner.Image = request.banner.Image;
            banner.Status = request.banner.Status;

            int Val = 0;

            banner.BannerId = BBanner.Insert(banner, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Banner", banner.BannerId, 1, baseRequest.Session.UserId);

            //Requered Update File
            string path = BannerPath + "/" + banner.BannerId.ToString() + request.banner.ImageExtension;
            banner.Image = path;
            BBanner.Update(banner, ref Val);

            byte[] File = request.banner.FileByte;

            Uri webRootUri = new Uri(webRoot);
            string pathAbs = webRootUri.AbsolutePath;
            var pathSave = pathAbs + rootPath + path;

            if (!Directory.Exists(pathAbs + rootPath + BannerPath)) Directory.CreateDirectory(pathAbs + rootPath + BannerPath);

            if (System.IO.File.Exists(pathSave)) System.IO.File.Delete(pathSave);

            System.IO.File.WriteAllBytes(pathSave, File);

            if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, BannerPath, banner.BannerId.ToString() + request.banner.ImageExtension))
                //if (!BAplication.AWS_Upload_S3(pathSave, "unitlifebucket", BannerPath, banner.BannerId.ToString() + request.banner.ImageExtension))
            {
                response.Message = String.Format(Messages.ErrorLoadPhoto, "Banner");
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
                response.Message = String.Format(Messages.ErrorInsert, "Banner");
            }

            response.banner = banner;

            return response;
        }


        [HttpPost]
        [Route("0/UpdateBanner")]
        public BannerResponse UpdateBanner([FromBody] BannerRequest request)
        {
            BannerResponse response = new BannerResponse();
            MBanner ent = new MBanner();

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
            string BannerPath = _appSettings.Value.BannerPath + "/";

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;

            ent.BannerId = request.banner.BannerId;
            ent.Title = request.banner.Title;
            ent.SubTile = request.banner.SubTile;
            ent.Image = BannerPath + ((request.banner.FileByte != null) ? request.banner.BannerId.ToString() + request.banner.ImageExtension : request.banner.Image);
            ent.Status = request.banner.Status;

            int Val = 0;

            ent.BannerId = BBanner.Update(ent, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Banner", ent.BannerId, 2, baseRequest.Session.UserId);

            if (request.banner.FileByte != null)
            {

                Uri webRootUri = new Uri(webRoot);
                string pathAbs = webRootUri.AbsolutePath;
                var pathSave = pathAbs + rootPath + ent.Image;

                byte[] File = request.banner.FileByte;

                if (!Directory.Exists(pathAbs + rootPath + BannerPath))
                {
                    Directory.CreateDirectory(pathAbs + rootPath + BannerPath);
                }

                if (System.IO.File.Exists(pathSave))
                {
                    System.IO.File.Delete(pathSave);
                }

                System.IO.File.WriteAllBytes(pathSave, File);

                string ImageFileName = ent.BannerId.ToString() + request.banner.ImageExtension;
                
                if (!BAwsSDK.UploadS3(_MAwsS3, pathSave, BannerPath.Replace("/", string.Empty), ImageFileName))
                    //if (!BAplication.AWS_Upload_S3(pathSave, "unitlifebucket", BannerPath.Replace("/", string.Empty), ImageFileName))
                {
                    response.Message = String.Format(Messages.ErrorLoadPhoto, "Banner");
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

            response.banner = ent;

            return response;
        }

        [HttpPost]
        [Route("0/GetBanner")]
        public BannerResponse GetBanner([FromBody] BannerRequest request)
        {
            BannerResponse response = new BannerResponse();
            MBanner ent = new MBanner();
            string BannerPath = _appSettings.Value.BannerPath + "/";

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

            ent.BannerId = request.banner.BannerId;

            int Val = 0;

            ent = BBanner.Select(ent, ref Val);
            ent.Image = ent.Image.Replace(BannerPath, string.Empty);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(1))
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NoExistsSelect, "Banner");
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorSelect, "Banner");
            }

            response.banner = ent;

            return response;
        }


        [HttpPost]
        [Route("0/FilterBanners")]
        public BannersResponse FilterBanners([FromBody] BannerRequest request)
        {
            BannersResponse response = new BannersResponse();
            List<MBanner> ents = new List<MBanner>();
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

            MBanner ent = new MBanner();

            ent.Title = request.banner.Title;
            ent.Status = request.banner.Status;

            int Val = 0;

            ents = BBanner.List(ent, ref Val);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorObtainingReults, "Banners");
            }
            else
            {
                response.Code = "1"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.NotReults, "Banners");
            }

            response.banners = ents.ToArray();

            return response;
        }

        [HttpPost]
        [Route("0/DeleteBanner")]
        public BannerResponse DeleteBanner([FromBody] BannerRequest request)
        {
            BannerResponse response = new BannerResponse();
            MBanner ent = new MBanner();

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

            ent.BannerId = request.banner.BannerId;

            int Val = 0;

            ent.BannerId = BBanner.Delete(ent, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("Banner", ent.BannerId, 3, baseRequest.Session.UserId);

            if (Val.Equals(0))
            {
                response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = Messages.Success;
            }
            else if (Val.Equals(2))
            {
                response.Code = "2"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
                response.Message = String.Format(Messages.ErrorDelete, "Banner");
            }

            response.banner = ent;

            return response;
        }
    }
}