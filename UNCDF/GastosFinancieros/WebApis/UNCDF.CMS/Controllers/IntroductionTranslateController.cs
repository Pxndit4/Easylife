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
    public class IntroductionTranslateController : Controller
    {
        // GET: IntroductionTranslate
        public ActionResult Index()
        {
            return View();
        }

        #region Translation
        public ActionResult NewTranslation(string id)
        {
            ViewBag.Title = "Register Introduction Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Introduction Transalate");
            IntroductionTranslateViewModel viewModel = new IntroductionTranslateViewModel();
            MIntroduction objResult;


            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiIntroduction().GetIntroduction(new MIntroduction
                {
                    IntroductionId = id.ToInt32()
                }, objSession);

                ViewBag.Languages = new WebApiLanguage().GetLanguages(new MLanguage
                {
                    Description = string.Empty,
                    Status = 1
                }, objSession).Where(x => x.LanguageId != Extension.GetIdLanguageENG()).Select(x => new SelectListItem
                {
                    Value = (x.LanguageId).ToString(),
                    Text = x.Description
                });

                viewModel.IntroductionId = id.ToInt32();
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
        public ActionResult Register(IntroductionTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            MIntroductionTranslate eIntroductionTransalate = new MIntroductionTranslate();

            try
            {
                MIntroductionTranslate objEnt = new MIntroductionTranslate
                {
                    IntroductionId = model.IntroductionId,
                    LanguageId = model.LanguageId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim()
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eIntroductionTransalate = new WebApiIntroductionTranslate().GetIntroductionTranslate(new MIntroductionTranslate
                {
                    IntroductionId = model.IntroductionId,
                    LanguageId = model.LanguageId
                }, objSession);

                if (eIntroductionTransalate.LanguageId > 0)
                {
                    objResult.isError = true;
                    objResult.message = string.Format("This translation already exists for this language", "Introduction Translate");
                    return Json(objResult);
                }


                response = new WebApiIntroductionTranslate().InsertIntroductionTranslate(objEnt, objSession);


                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Introduction Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.IntroductionId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Update(IntroductionTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MIntroductionTranslate objEnt = new MIntroductionTranslate
                {
                    IntroductionId = model.IntroductionId,
                    LanguageId = model.LanguageId,
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    Title = Extension.ToEmpty(model.Title).Trim()
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiIntroductionTranslate().UpdateIntroductionTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Introduction"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.IntroductionId == 0)
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
                MIntroductionTranslate eIntroduction = new MIntroductionTranslate();
                List<MIntroductionTranslate> eIntroductions = new List<MIntroductionTranslate>();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eIntroduction.IntroductionId = id.ToInt32();


                eIntroductions = new WebApiIntroductionTranslate().GetIntroductionTranslates(eIntroduction, objSession);


                objResult.data = eIntroductions.Select(x => new MIntroductionTranslate
                {
                    IntroductionId = x.IntroductionId,
                    LanguageId = x.LanguageId,
                    Language = x.Language,
                    Description = x.Description,
                    Title = x.Title
                }).ToList();


                //  objResult.data = eIntroductions;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Introduction");
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
                MIntroductionTranslate objEnt = new MIntroductionTranslate();
                objEnt.IntroductionId = Convert.ToInt32(id);
                objEnt.LanguageId = Convert.ToInt32(LanguageId);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                response = new WebApiIntroductionTranslate().DeleteIntroduction(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Introduction Translate"); ;
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