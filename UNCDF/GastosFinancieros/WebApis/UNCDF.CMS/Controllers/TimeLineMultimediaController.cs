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
    public class TimeLineMultimediaController : Controller
    {
      
        [HttpPost]
        public JsonResult Search(TimeLineViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string MultimediaPath = ConfigurationManager.AppSettings["URLMultimedia"].ToString();
            try
            {
                MTimeLineMultimedia eTimeLineMultimedia = new MTimeLineMultimedia();
                List<MTimeLineMultimedia> eTimeLineMultimedias = new List<MTimeLineMultimedia>();

                eTimeLineMultimedia.TimeLineId = model.TimeLineId;
                eTimeLineMultimedia.Title = Extension.ToEmpty(model.Title).Trim();
                eTimeLineMultimedia.Type = -1;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eTimeLineMultimedias = new WebApiTimeLineMultimedia().GetTimeLineMultimedias(eTimeLineMultimedia, objSession);

                //(objResult.Photo.Replace(Extension.S3Server, "")).Replace(OngPath, "");
                objResult.data = eTimeLineMultimedias.Select(x => new MTimeLineMultimedia
                {
                    TimeLineMulId = x.TimeLineMulId,
                    TimeLineId = x.TimeLineId,
                    Title = x.Title,
                    Type = x.Type,
                    TypeName = (x.Type == 1) ? "Image" : "Video",
                    File = (x.File.Replace(Extension.S3Server, "")).Replace(MultimediaPath, ""),
                    FileLink = x.File
                }).ToList();

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "TimeLineMultimedia");
            }

            return Json(objResult);
        }

        public ActionResult New(string id)
        {
            ViewBag.Title = "Register Time Line Multimedia";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "TimeLineMultimedia");
            TimeLineMultimediaViewModel viewModel = new TimeLineMultimediaViewModel();
            try
            {
                ViewBag.TypeFile = Extension.GetTypeFile().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });


                viewModel.TimeLineId = id.ToInt32();
                viewModel.FileLink = string.Empty;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Register", viewModel);
        }


        public ActionResult Edit(string id)
        {
            MTimeLineMultimedia objResult;
            //BE.EProject objProject;
            ViewBag.Title = "Edit TimeLineMultimedia";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "TimeLineMultimedia");
            try
            {
                ViewBag.TypeFile = Extension.GetTypeFile().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MTimeLineMultimedia eTimeLineMultimedia = new MTimeLineMultimedia
                {
                    TimeLineMulId = Convert.ToInt32(id)
                };

                objResult = new WebApiTimeLineMultimedia().GetTimeLineMultimedia(eTimeLineMultimedia, objSession);

                //objProject = new WebApiProject().GetProject(new BE.EProject
                //{
                //    ProjectId = objResult.ProjectId
                //});

                return View("Register", new TimeLineMultimediaViewModel()
                {
                    TimeLineMulId = objResult.TimeLineMulId,
                    TimeLineId = objResult.TimeLineId,
                    Title = objResult.Title,
                    Type = objResult.Type,
                    File = objResult.File,
                    FileLink = (string.IsNullOrEmpty(objResult.File)) ? string.Empty : Extension.S3Server + Extension.pathMultimedia + objResult.File
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult Register(TimeLineMultimediaViewModel model, HttpPostedFileBase imageFile)
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

                MTimeLineMultimedia objEnt = new MTimeLineMultimedia
                {
                    TimeLineId = model.TimeLineId,
                    TimeLineMulId = model.TimeLineMulId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    Type = model.Type,
                    File = model.File,
                    FileByte = imgData,
                    FileExt = Ext
                };

                if (model.TimeLineMulId == 0)
                    response = new WebApiTimeLineMultimedia().InsertTimeLineMultimedia(objEnt, objSession);
                else
                    response = new WebApiTimeLineMultimedia().UpdateTimeLineMultimedia(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "TimeLineMultimedia"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.TimeLineMulId == 0)
                    objResult.message = MessageResource.SaveError;
                else
                    objResult.message = MessageResource.UpdateError;
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
                MTimeLineMultimedia objEnt = new MTimeLineMultimedia();
                objEnt.TimeLineMulId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiTimeLineMultimedia().DeleteTimeLineMultimedia(objEnt, objSession); //Falta crear el metodo de editar

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