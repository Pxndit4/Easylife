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
    public class GenderController : Controller
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
        public JsonResult Search(SearchGenderViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MGender eGender = new MGender();
                List<MGender> eGenders = new List<MGender>();

                eGender.Description = Extension.ToEmpty(model.Description).Trim();
                eGender.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eGenders = new WebApiGender().GetGenders(eGender, objSession);

                objResult.data = eGenders.Select(x => new MGender
                {
                    GenderId = x.GenderId,
                    Description = x.Description,
                    Status = x.Status,
                    Value = x.Value,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Gender");
            }

            return Json(objResult);
        }

        public ActionResult New()
        {
            ViewBag.Title = "Register Gender";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Gender");
            GenderViewModel model = new GenderViewModel();
            try
            {
                model.Status = 1; //default
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
            return View("Register", model);
        }

        public ActionResult Edit(string id)
        {
            MGender objResult;
            ViewBag.Title = "Edit Gender";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Gender");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MGender eGender = new MGender
                {
                    GenderId = Convert.ToInt32(id)
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiGender().GetGender(eGender, objSession);

                return View("Register", new GenderViewModel()
                {
                    GenderId = objResult.GenderId,
                    Description = objResult.Description,
                    Value = objResult.Value,
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
        public ActionResult Register(GenderViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MGender objEnt = new MGender
                {
                    GenderId = model.GenderId,
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    Value = Extension.ToEmpty(model.Value).Trim(),
                    Status = model.Status
                };

                if (model.GenderId == 0)
                    response = new WebApiGender().InsertGender(objEnt, objSession);
                else
                    response = new WebApiGender().UpdateGender(objEnt, objSession);

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
        public JsonResult Delete(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MGender objEnt = new MGender();
                objEnt.GenderId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                response = new WebApiGender().DeleteGender(objEnt, objSession); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Gender"); ;
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