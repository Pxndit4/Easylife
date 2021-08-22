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
    public class GenderTranslateController : Controller
    {
        // GET: GenderTranslate
        public ActionResult Index()
        {
            return View();
        }

        #region Translation
        public ActionResult NewTranslation(string id)
        {
            ViewBag.Title = "Register Gender Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Gender Transalate");
            GenderTranslateViewModel viewModel = new GenderTranslateViewModel();
            MGender objResult;


            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                objResult = new WebApiGender().GetGender(new MGender
                {
                    GenderId = id.ToInt32()
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

                viewModel.GenderId = id.ToInt32();
                viewModel.DescriptionName = objResult.Description;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Register", viewModel);
        }


        [HttpPost]
        public ActionResult Register(GenderTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            MGenderTranslate eGenderTransalate = new MGenderTranslate();

            try
            {
                MGenderTranslate objEnt = new MGenderTranslate
                {
                    GenderId = model.GenderId,
                    LanguageId = model.LanguageId,
                    Value = Extension.ToEmpty(model.Value).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim()
                };


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eGenderTransalate = new WebApiGenderTranslate().GetGenderTranslate(new MGenderTranslate
                {
                    GenderId = model.GenderId,
                    LanguageId = model.LanguageId
                }, objSession);

                if (eGenderTransalate.LanguageId > 0)
                {
                    objResult.isError = true;
                    objResult.message = string.Format("This translation already exists for this language", "Gender Translate");
                    return Json(objResult);
                }


                response = new WebApiGenderTranslate().InsertGenderTranslate(objEnt, objSession);


                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Gender Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.GenderId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Update(GenderTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MGenderTranslate objEnt = new MGenderTranslate
                {
                    GenderId = model.GenderId,
                    LanguageId = model.LanguageId,
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    Value = Extension.ToEmpty(model.Value).Trim()
                };


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiGenderTranslate().UpdateGenderTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Gender"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.GenderId == 0)
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
                MGenderTranslate eGender = new MGenderTranslate();
                List<MGenderTranslate> eGenders = new List<MGenderTranslate>();

                eGender.GenderId = id.ToInt32();


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eGenders = new WebApiGenderTranslate().GetGenderTranslates(eGender, objSession);


                objResult.data = eGenders.Select(x => new MGenderTranslate
                {
                    GenderId = x.GenderId,
                    LanguageId = x.LanguageId,
                    Language = x.Language,
                    Description = x.Description,
                    Value = x.Value
                }).ToList();


                //  objResult.data = eGenders;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Gender");
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
                MGenderTranslate objEnt = new MGenderTranslate();
                objEnt.GenderId = Convert.ToInt32(id);
                objEnt.LanguageId = Convert.ToInt32(LanguageId);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiGenderTranslate().DeleteGender(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Gender Translate"); ;
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