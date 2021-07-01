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
    public class TimeLineTestimonialController : Controller
    {

        [HttpPost]
        public JsonResult Search(TimeLineViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string TestPath = ConfigurationManager.AppSettings["URLTestPath"].ToString();
            try
            {
                MTimeLineTestimonial eTimeLineTestimonial = new MTimeLineTestimonial();
                List<MTimeLineTestimonial> eTimeLineTestimonials = new List<MTimeLineTestimonial>();

                eTimeLineTestimonial.TimeLineId = model.TimeLineId;
                eTimeLineTestimonial.Name = string.Empty; // Extension.ToEmpty(model.Name);


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eTimeLineTestimonials = new WebApiTimeLineTestimonial().GetTimeLineTestimonials(eTimeLineTestimonial, objSession);

                objResult.data = eTimeLineTestimonials.Select(x => new MTimeLineTestimonial
                {
                    TimeLineTestId = x.TimeLineTestId,
                    TimeLineId = x.TimeLineId,
                    Name = x.Name,
                    Photo = (x.Photo.Replace(Extension.S3Server, "")).Replace(TestPath, ""),
                    PhotoLink = x.Photo
                }).ToList();

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "TimeLineTestimonial");
            }

            return Json(objResult);
        }

        public ActionResult New(string id)
        {
            ViewBag.Title = "Register Time Line Testimonial";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "TimeLineTestimonial");
            TimeLineTestimonialViewModel viewModel = new TimeLineTestimonialViewModel();
            try
            {

                //ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                //{
                //    Value = x.Id,
                //    Text = x.Value
                //});

                viewModel.TimeLineId = id.ToInt32();
                viewModel.PhotoLink = string.Empty;
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
            MTimeLineTestimonial objResult;
            //BE.EProject objProject;
            ViewBag.Title = "Edit TimeLine Testimonial";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "TimeLineTestimonial");
            try
            {
                //ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                //{
                //    Value = x.Id,
                //    Text = x.Value
                //});

                MTimeLineTestimonial eTimeLineTestimonial = new MTimeLineTestimonial
                {
                    TimeLineTestId = Convert.ToInt32(id)
                };


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiTimeLineTestimonial().GetTimeLineTestimonial(eTimeLineTestimonial, objSession);

                // objResult = new WebApiTimeLineTestimonial().GetTimeLineTestimonial(eTimeLineTestimonial);

                return View("Register", new TimeLineTestimonialViewModel()
                {
                    TimeLineTestId = objResult.TimeLineTestId,
                    TimeLineId = objResult.TimeLineId,
                    Name = objResult.Name,
                    Photo = objResult.Photo,
                    PhotoLink = (string.IsNullOrEmpty(objResult.Photo)) ? string.Empty : Extension.S3Server + Extension.pathTestimonial + objResult.Photo,
                    Testimonial = objResult.Testimonial
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult Register(TimeLineTestimonialViewModel model, HttpPostedFileBase imageFile)
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

                MTimeLineTestimonial objEnt = new MTimeLineTestimonial
                {
                    TimeLineId = model.TimeLineId,
                    TimeLineTestId = model.TimeLineTestId,
                    Name = Extension.ToEmpty(model.Name).Trim(),
                    Photo = Extension.ToEmpty(model.Photo),
                    Testimonial = Extension.ToEmpty(model.Testimonial).Trim(),
                    FileByte = imgData,
                    Ext = Ext
                };

                if (model.TimeLineTestId == 0)
                    response = new WebApiTimeLineTestimonial().InsertTimeLineTestimonial(objEnt, objSession);
                else
                    response = new WebApiTimeLineTestimonial().UpdateTimeLineTestimonial(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "TimeLineTestimonial"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.TimeLineTestId == 0)
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

                MTimeLineTestimonial objEnt = new MTimeLineTestimonial();
                objEnt.TimeLineTestId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiTimeLineTestimonial().DeleteTimeLineTestimonial(objEnt, objSession); //Falta crear el metodo de editar

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