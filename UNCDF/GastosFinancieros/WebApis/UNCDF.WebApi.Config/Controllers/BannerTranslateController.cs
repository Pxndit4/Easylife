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
    [EnableCors("AllowMyOrigin")]
    public class BannerTranslateController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("0/GetBannerTranslates")]
        public BannerTranslatesResponse GetBannerTranslates([FromBody] BannerTranslateRequest request)
        {
            BannerTranslatesResponse response = new BannerTranslatesResponse();
            List<MBannerTranslate> BannerTranslateList = new List<MBannerTranslate>();
            MBannerTranslate MBannerTranslate = new MBannerTranslate();
            BaseRequest baseRequest = new BaseRequest();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/


            MBannerTranslate.BannerId = request.MBannerTranslate.BannerId;

            baseRequest.Language = request.Language;
            baseRequest.Session = request.Session;
            int Val = 0;

            BannerTranslateList = BBannerTranslate.Lis(MBannerTranslate, ref Val);

            response.Code = "0"; //0=> Ëxito | 1=> Validación de Sistema | 2 => Error de Excepción
            response.Message = Messages.Success;
            response.BannerTranslates = BannerTranslateList.ToArray();


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

            return response;
        }


        [HttpPost]
        [Route("0/GetBannerTranslate")]
        public BannerTranslateResponse GetBannerTranslate([FromBody] BannerTranslateRequest request)
        {
            BannerTranslateResponse response = new BannerTranslateResponse();
            MBannerTranslate genderbe = new MBannerTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Session = request.Session;

            int Val = 0;

            genderbe.BannerId = request.MBannerTranslate.BannerId;
            genderbe.LanguageId = request.MBannerTranslate.LanguageId;

            genderbe = BBannerTranslate.Select(genderbe, ref Val);

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

            response.BannerTranslate = genderbe;

            return response;
        }

        [HttpPost]
        [Route("0/InsertBannerTranslate")]
        public BannerTranslateResponse InsertBannerTranslate([FromBody] BannerTranslateRequest request)
        {
            BannerTranslateResponse response = new BannerTranslateResponse();
            MBannerTranslate MBannerTranslate = new MBannerTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            int Val = 0;

            MBannerTranslate.BannerId = request.MBannerTranslate.BannerId;
            MBannerTranslate.LanguageId = request.MBannerTranslate.LanguageId;
            MBannerTranslate.Title = request.MBannerTranslate.Title;
            MBannerTranslate.SubTitle = request.MBannerTranslate.SubTitle;


            MBannerTranslate.BannerId = BBannerTranslate.Insert(MBannerTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("BannerTranslate", MBannerTranslate.BannerId, 1, baseRequest.Session.UserId);

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

            response.BannerTranslate = MBannerTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/UpdateBannerTranslate")]
        public BannerTranslateResponse UpdateBannerTranslate([FromBody] BannerTranslateRequest request)
        {
            BannerTranslateResponse response = new BannerTranslateResponse();
            MBannerTranslate MBannerTranslate = new MBannerTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();


            baseRequest.Session = request.Session;

            int Val = 0;

            MBannerTranslate.BannerId = request.MBannerTranslate.BannerId;
            MBannerTranslate.LanguageId = request.MBannerTranslate.LanguageId;
            MBannerTranslate.Title = request.MBannerTranslate.Title;
            MBannerTranslate.SubTitle = request.MBannerTranslate.SubTitle;


            MBannerTranslate.BannerId = BBannerTranslate.Update(MBannerTranslate, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("BannerTranslate", MBannerTranslate.BannerId, 2, baseRequest.Session.UserId);

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

            response.BannerTranslate = MBannerTranslate;

            return response;
        }

        [HttpPost]
        [Route("0/DeleteBannerTranslate")]
        public BannerTranslateResponse DeleteBannerTranslate([FromBody] BannerTranslateRequest request)
        {
            BannerTranslateResponse response = new BannerTranslateResponse();
            MBannerTranslate genderbe = new MBannerTranslate();

            /*METODO QUE VALIDA EL TOKEN DE APLICACIÓN*/
            if (!BAplication.ValidateAplicationToken(request.ApplicationToken))
            {
                response.Code = "2";
                response.Message = Messages.ApplicationTokenNoAutorize;
                return response;
            }
            /*************FIN DEL METODO*************/

            BaseRequest baseRequest = new BaseRequest();

            baseRequest.Session = request.Session;

            int Val = 0;

            genderbe.BannerId = request.MBannerTranslate.BannerId;
            genderbe.LanguageId = request.MBannerTranslate.LanguageId;

            genderbe.BannerId = BBannerTranslate.Delete(genderbe, ref Val);

            //Record the audit
            Val = BAudit.RecordAudit("BannerTranslate", genderbe.BannerId, 3, baseRequest.Session.UserId);

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

            response.BannerTranslate = genderbe;

            return response;
        }

    }

}