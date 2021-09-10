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
    public class IntroductionController : Controller
    {
        // GET: Introduction
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
        public JsonResult Search(SearchIntroductionViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MIntroduction eIntroduction = new MIntroduction();
                List<MIntroduction> eIntroductions = new List<MIntroduction>();

                eIntroduction.Title = Extension.ToEmpty(model.Title).Trim();
                eIntroduction.Description = string.Empty;
                eIntroduction.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eIntroductions = new WebApiIntroduction().GetIntroductions(eIntroduction, objSession);

                objResult.data = eIntroductions.Select(x => new MIntroduction
                {
                    IntroductionId = x.IntroductionId,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    Order = x.Order,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

                // objResult.data = eIntroductions;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Introduction");
            }

            return Json(objResult);
        }

        public ActionResult New()
        {
            ViewBag.Title = "Register Introduction";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Introduction");
            int vOrder = 0;

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

                /*For Order*/
                MIntroduction eIntroduction = new MIntroduction();
                List<MIntroduction> eIntroductions = new List<MIntroduction>();

                eIntroduction.Title = string.Empty;
                eIntroduction.Description = string.Empty;
                eIntroduction.Status = 1;

                eIntroductions = new WebApiIntroduction().GetIntroductions(eIntroduction, objSession).Where(x => x.Order != null).ToList();

                vOrder = eIntroductions.Count() + 1;
                /*For Order*/


                //ViewBag.Order = Extension.GetOrder().Select(x => new SelectListItem
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
            return View("Register", new IntroductionViewModel() { Status = 1, Order = vOrder, ImageLink = string.Empty });
        }

        public ActionResult Edit(string id)
        {
            MIntroduction objResult;
            ViewBag.Title = "Edit Introduction";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Introduction");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MIntroduction eIntroduction = new MIntroduction
                {
                    IntroductionId = Convert.ToInt32(id)
                };
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                objResult = new WebApiIntroduction().GetIntroduction(eIntroduction, objSession);

                return View("Register", new IntroductionViewModel()
                {
                    IntroductionId = objResult.IntroductionId,
                    Title = objResult.Title,
                    Description = objResult.Description,
                    Image = objResult.Image,
                    Order = (objResult.Status == 0) ? 99 : objResult.Order,
                    ImageLink = (string.IsNullOrEmpty(objResult.Image)) ? string.Empty : Extension.S3Server + Extension.pathIntroduction + objResult.Image,
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
        public ActionResult Register(IntroductionViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;
            string fileName = string.Empty;
            string Ext = string.Empty;

            byte[] imgData = null;//; new byte[0];


            try
            {
                if (imageFile != null)
                {
                    fileName = imageFile.FileName;
                    Ext = Path.GetExtension(imageFile.FileName);
                    imgData = Extension.FileToByteArray(imageFile);

                }

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MIntroduction objEnt = new MIntroduction
                {
                    IntroductionId = model.IntroductionId,
                    Title = Extension.ToEmpty(model.Title),
                    Description = Extension.ToEmpty(model.Description),
                    Order = model.Order,
                    Image = model.Image,
                    Status = model.Status,
                    ImageExtension = Ext,
                    FileByte = imgData
                };

                if (model.IntroductionId == 0)
                {
                    response = new WebApiIntroduction().InsertIntroduction(objEnt, objSession);
                }
                else
                {
                    response = new WebApiIntroduction().UpdateIntroduction(objEnt, objSession);
                }


                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = false;//statusCode.Equals("2") ? true : false;
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
        public JsonResult Delete(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MIntroduction objEnt = new MIntroduction();
                objEnt.IntroductionId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiIntroduction().DeleteIntroduction(objEnt, objSession); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Introduction"); ;
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