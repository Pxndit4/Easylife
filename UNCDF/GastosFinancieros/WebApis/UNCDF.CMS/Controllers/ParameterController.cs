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
    public class ParameterController : Controller
    {
        public ActionResult Index()
        {
            Session objSession = new Session()
            {
                UserId = AutenticationManager.GetUser().IdUsuario,
            };

            var listCode = ((new WebApiParameter().GetParameters(new MParameter
            {
                Code = String.Empty

            }, objSession)).Select(y => y.Code).Distinct().ToList()).Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();

            listCode.Insert(0, new SelectListItem { Text = "   Select   ", Value = "", Selected = true });

            ViewBag.Code = listCode;


            return View();
        }

        [HttpPost]
        public JsonResult Search(SearchParameterViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MParameter eParameter = new MParameter();
                List<MParameter> eParameters = new List<MParameter>();

                eParameter.Code = Extension.ToEmpty(model.Code);
                eParameter.Description = string.Empty;
                //eParameter.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eParameters = new WebApiParameter().GetParameters(eParameter, objSession);

                objResult.data = eParameters.Select(x => new MParameter
                {
                    ParameterId = x.ParameterId,
                    Description = x.Description,
                    Code = x.Code,
                    Valor1 = x.Valor1,
                    Valor2 = x.Valor2,
                    Status = x.Status
                    //StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Parameter");
            }

            return Json(objResult);
        }


        [HttpPost]
        public JsonResult getParameter(string code, string description)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MParameter eParameter = new MParameter();
                List<MParameter> eParameters = new List<MParameter>();

                eParameter.Code = Extension.ToEmpty(code);
                eParameter.Description = Extension.ToEmpty(description);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eParameters = new WebApiParameter().GetParameters(eParameter, objSession);

                eParameter = eParameters.Select(x => new MParameter
                {
                    ParameterId = x.ParameterId,
                    Description = x.Description,
                    Code = x.Code,
                    Valor1 = x.Valor1,
                    Valor2 = x.Valor2,
                    Status = x.Status
                }).FirstOrDefault();

                objResult.data = eParameter;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Parameter");
            }

            return Json(objResult);
        }


        public ActionResult New()
        {
            ViewBag.Title = "Register Parameter";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Parameter");
            ParameterViewModel model = new ParameterViewModel();
            try
            {
                //model.Status = 1; //default
                //ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                //{
                //    Value = x.Id,
                //    Text = x.Value
                //});
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
            MParameter objResult;
            ViewBag.Title = "Edit Parameter";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Parameter");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MParameter eParameter = new MParameter
                {
                    ParameterId = Convert.ToInt32(id)
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiParameter().GetParameter(eParameter, objSession);

                return View("Register", new ParameterViewModel()
                {
                    ParameterId = objResult.ParameterId,
                    Description = objResult.Description,
                    Status = objResult.Status,
                    Code = objResult.Code,
                    Valor1 = objResult.Valor1,
                    Valor2 = objResult.Valor2,
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult Register(ParameterViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MParameter objEnt = new MParameter
                {
                    ParameterId = model.ParameterId,
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    Code = Extension.ToEmpty(model.Code).Trim(),
                    Valor1 = Extension.ToEmpty(model.Valor1).Trim(),
                    Valor2 = Extension.ToEmpty(model.Valor2).Trim(),
                    Status = model.Status
                };

                //if (model.ParameterId == 0)
                //    response = new WebApiParameter().InsertParameter(objEnt, objSession);
                //else
                response = new WebApiParameter().UpdateParameter(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Parameter"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.ParameterId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }
    }
}