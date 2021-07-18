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
    public class BannerController : Controller
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
        public JsonResult Search(SearchBannerViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MBanner eBanner = new MBanner();
                List<MBanner> eBanners = new List<MBanner>();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eBanner.Title = Extension.ToEmpty(model.Title).Trim();
                eBanner.Status = model.Status;

                eBanners = new WebApiBanner().GetBanners(eBanner, objSession);
                objResult.data = eBanners.Select(x => new MBanner
                {
                    BannerId = x.BannerId,
                    Title = x.Title,
                    SubTile = x.SubTile,
                    Image = x.Image,
                    Status = x.Status,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

                //objResult.data = eBanners;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Banner");
            }

            return Json(objResult);
        }

        public ActionResult New()
        {
            ViewBag.Title = "Register Banner";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Banner");

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
            return View("Register", new BannerViewModel() { Status = 1, ImageLink = String.Empty });
        }

        public ActionResult Edit(string id)
        {
            MBanner objResult;
            ViewBag.Title = "Edit Banner";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Banner");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MBanner eBanner = new MBanner
                {
                    BannerId = Convert.ToInt32(id)
                };

                objResult = new WebApiBanner().GetBanner(eBanner, objSession);
                //string statusCode = response.Split('|')[0];
                //string statusMessage = response.Split('|')[1];

                return View("Register", new BannerViewModel()
                {
                    BannerId = objResult.BannerId,
                    Title = objResult.Title.Trim(),
                    SubTile = objResult.SubTile.Trim(),
                    Image = objResult.Image,
                    ImageLink = (string.IsNullOrEmpty(objResult.Image)) ? string.Empty : Extension.S3Server + Extension.pathBanner + objResult.Image,
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
        [ValidateInput(false)]
        public ActionResult Register(BannerViewModel model, HttpPostedFileBase imageFile)
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

                MBanner objEnt = new MBanner
                {
                    BannerId = model.BannerId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    SubTile = Extension.ToEmpty(model.SubTile).Trim(),
                    Image = model.Image,
                    Status = model.Status,
                    ImageExtension = Ext,
                    FileByte = imgData
                };



                if (model.BannerId == 0)
                {
                    response = new WebApiBanner().InsertBanner(objEnt, objSession);
                }
                else
                {
                    response = new WebApiBanner().UpdateBanner(objEnt, objSession);
                }


                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = false;//statusCode.Equals("2") ? true : false;
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
        public JsonResult Delete(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MBanner objEnt = new MBanner();
                objEnt.BannerId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiBanner().DeleteBanner(objEnt, objSession); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Banner"); ;
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