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
    public class LanguageController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            });

            return View();
        }

        [HttpPost]
        public JsonResult Search(SearchLanguageViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MLanguage eLanguage = new MLanguage();
                List<MLanguage> eLanguages = new List<MLanguage>();

                eLanguage.Description = Extension.ToEmpty(model.Description).Trim();
                eLanguage.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eLanguages = new WebApiLanguage().GetLanguages(eLanguage, objSession);

                objResult.data = eLanguages.Select(x => new MLanguage
                {
                    LanguageId = x.LanguageId,
                    Code = x.Code,
                    Description = x.Description,
                    Status = x.Status,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

                // objResult.data = eLanguages;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Language");
            }

            return Json(objResult);
        }

        public ActionResult New()
        {
            ViewBag.Title = "Register Language";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Language");

            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Register", new LanguageViewModel() { Status = 1, FlagLink = string.Empty });
        }

        public ActionResult Edit(string id)
        {
            MLanguage objResult;
            ViewBag.Title = "Edit Language";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Language");
            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MLanguage eLanguage = new MLanguage
                {
                    LanguageId = Convert.ToInt32(id)
                };

                objResult = new WebApiLanguage().GetLanguage(eLanguage, objSession);

                return View("Register", new LanguageViewModel()
                {
                    LanguageId = objResult.LanguageId,
                    Description = objResult.Description,
                    Flag = objResult.Flag,
                    FlagLink = (string.IsNullOrEmpty(objResult.Flag)) ? string.Empty : Extension.S3Server + Extension.pathLanguage + objResult.Flag,
                    FlagOld = objResult.Flag,
                    Code = objResult.Code,
                    Status = objResult.Status
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult Register(LanguageViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            string fileName = string.Empty;
            string Ext = string.Empty;


            byte[] imgData = null;//; new byte[0];

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                if (imageFile != null)
                {
                    fileName = imageFile.FileName;
                    Ext = Path.GetExtension(imageFile.FileName);
                    imgData = Extension.FileToByteArray(imageFile);

                }


                MLanguage objEnt = new MLanguage
                {
                    LanguageId = model.LanguageId,
                    Description = Extension.ToEmpty(model.Description.Trim()),
                    Flag = Extension.ToEmpty(model.Flag).Trim(),
                    Code = Extension.ToEmpty(model.Code).Trim(),
                    Status = model.Status,
                    FlagExtension = Ext,
                    FileByte = imgData
                };


                if (model.LanguageId == 0)
                {
                    response = new WebApiLanguage().InsertLanguage(objEnt, objSession);
                }
                else
                {
                    response = new WebApiLanguage().UpdateLanguage(objEnt, objSession);
                }


                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Language"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.LanguageId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MLanguage objEnt = new MLanguage();
                objEnt.LanguageId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiLanguage().DeleteLanguage(objEnt, objSession); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "User"); ;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = MessageResource.ControllerDeleteExceptionMessage;
            }

            return Json(objResult);
        }
    }
}