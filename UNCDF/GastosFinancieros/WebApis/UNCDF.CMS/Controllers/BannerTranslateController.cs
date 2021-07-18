using UNCDF.CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using UNCDF.Layers.Model;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;


namespace UNCDF.CMS.Controllers
{
    public class BannerTranslateController : Controller
    {
        // GET: BannerTranslate
        public ActionResult Index()
        {
            return View();
        }

        #region Translation
        public ActionResult NewTranslation(string id)
        {
            ViewBag.Title = "Register Banner Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Banner Transalate");
            BannerTranslateViewModel viewModel = new BannerTranslateViewModel();
            MBanner objResult;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiBanner().GetBanner(new MBanner
                {
                    BannerId = id.ToInt32()
                }, objSession);

                ViewBag.Languages = new WebApiLanguage().GetLanguages(new MLanguage
                {
                    Description = string.Empty
                    ,
                    Status = 1
                }, objSession).Where(x => x.LanguageId != Extension.GetIdLanguageENG()).Select(x => new SelectListItem
                {
                    Value = (x.LanguageId).ToString(),
                    Text = x.Description
                });

                viewModel.BannerId = id.ToInt32();
                viewModel.TitleName = objResult.Title;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Register", viewModel);
        }


        [HttpPost]
        public ActionResult Register(BannerTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            MBannerTranslate eBannerTransalate = new MBannerTranslate();

            try
            {
                MBannerTranslate objEnt = new MBannerTranslate
                {
                    BannerId = model.BannerId,
                    LanguageId = model.LanguageId,
                    SubTitle = Extension.ToEmpty(model.SubTitle).Trim(),
                    Title = Extension.ToEmpty(model.Title).Trim()
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eBannerTransalate = new WebApiBannerTranslate().GetBannerTranslate(new MBannerTranslate
                {
                    BannerId = model.BannerId,
                    LanguageId = model.LanguageId
                }, objSession);

                if (eBannerTransalate.LanguageId > 0)
                {
                    objResult.isError = true;
                    objResult.message = string.Format("This translation already exists for this language", "Banner Translate");
                    return Json(objResult);
                }


                response = new WebApiBannerTranslate().InsertBannerTranslate(objEnt, objSession);


                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Banner Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.BannerId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Update(BannerTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MBannerTranslate objEnt = new MBannerTranslate
                {
                    BannerId = model.BannerId,
                    LanguageId = model.LanguageId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    SubTitle = Extension.ToEmpty(model.SubTitle).Trim()
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                response = new WebApiBannerTranslate().UpdateBannerTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Banner"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.BannerId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }

        [HttpPost]
        public JsonResult Search(string id)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MBannerTranslate eBanner = new MBannerTranslate();
                List<MBannerTranslate> eBanners = new List<MBannerTranslate>();

                eBanner.BannerId = id.ToInt32();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eBanners = new WebApiBannerTranslate().GetBannerTranslates(eBanner, objSession);


                objResult.data = eBanners.Select(x => new MBannerTranslate
                {
                    BannerId = x.BannerId,
                    LanguageId = x.LanguageId,
                    Language = x.Language,
                    Title = x.Title,
                    SubTitle = x.SubTitle
                }).ToList();


                //  objResult.data = eBanners;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Banner");
            }

            return Json(objResult);
        }


        [HttpPost]
        public JsonResult Delete(string id, string LanguageId)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MBannerTranslate objEnt = new MBannerTranslate();
                objEnt.BannerId = Convert.ToInt32(id);
                objEnt.LanguageId = Convert.ToInt32(LanguageId);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiBannerTranslate().DeleteBanner(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Banner Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = MessageResource.ControllerDeleteExceptionMessage;
            }

            return Json(objResult);
        }
        #endregion Translation

    }
}