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
    public class TimeLineTestimonialTranslateController : Controller
    {
        // GET: TimeLineTestimonialTranslate
        public ActionResult Index()
        {
            return View();
        }

        #region Translation
        public ActionResult NewTranslation(string id)
        {
            ViewBag.Title = "Register Testimonial Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Testimonial Translation");
            TimeLineTestimonialTranslateViewModel viewModel = new TimeLineTestimonialTranslateViewModel();
            MTimeLineTestimonial objResult;


            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiTimeLineTestimonial().GetTimeLineTestimonial(new MTimeLineTestimonial
                {
                    TimeLineTestId = id.ToInt32()
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

                viewModel.TimeLineTestId = id.ToInt32();
                viewModel.Name = objResult.Name;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Register", viewModel);
        }

        public ActionResult ApproveTranslation(string id)
        {
            ViewBag.Title = "Testimonial Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Testimonial Translation");
            TimeLineTestimonialTranslateViewModel viewModel = new TimeLineTestimonialTranslateViewModel();
            MTimeLineTestimonial objResult;


            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiTimeLineTestimonial().GetTimeLineTestimonial(new MTimeLineTestimonial
                {
                    TimeLineTestId = id.ToInt32()
                }, objSession);

                ViewBag.Languages = new WebApiLanguage().GetLanguages(new MLanguage
                {
                    Description = string.Empty,
                    Status = 1
                }, objSession).Select(x => new SelectListItem
                {
                    Value = (x.LanguageId).ToString(),
                    Text = x.Description
                });

                viewModel.TimeLineTestId = id.ToInt32();
                viewModel.Name = objResult.Name;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Approve", viewModel);
        }

        [HttpPost]
        public ActionResult Register(TimeLineTestimonialTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            MTimeLineTestimonialTranslate eTimeLineTransalate = new MTimeLineTestimonialTranslate();

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MTimeLineTestimonialTranslate objEnt = new MTimeLineTestimonialTranslate
                {
                    TimeLineTestId = model.TimeLineTestId,
                    LanguageId = model.LanguageId,
                    Testimonial = Extension.ToEmpty(model.Testimonial).Trim()
                };

                eTimeLineTransalate = new WebApiTimeLineTestimonialTranslate().GetTimeLineTestimonialTranslate(new MTimeLineTestimonialTranslate
                {
                    TimeLineTestId = model.TimeLineTestId,
                    LanguageId = model.LanguageId
                }, objSession);

                if (eTimeLineTransalate.LanguageId > 0)
                {
                    objResult.isError = true;
                    objResult.message = string.Format("This translation already exists for this language", "Time Line Testimonial Translate");
                    return Json(objResult);
                }


                response = new WebApiTimeLineTestimonialTranslate().InsertTimeLineTestimonialTranslate(objEnt, objSession);

                //    response = null;//new WebApiTimeLineTestimonialTranslate().UpdateTimeLine(objEnt);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Time Line Testimonial Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.TimeLineTestId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Update(TimeLineTestimonialTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MTimeLineTestimonialTranslate objEnt = new MTimeLineTestimonialTranslate
                {
                    TimeLineTestId = model.TimeLineTestId,
                    LanguageId = model.LanguageId,
                    Testimonial = Extension.ToEmpty(model.Testimonial).Trim()
                };

                response = new WebApiTimeLineTestimonialTranslate().UpdateTimeLineTestimonialTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "TimeLine"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.TimeLineTestId == 0)
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
                MTimeLineTestimonialTranslate eTimeLine = new MTimeLineTestimonialTranslate();
                List<MTimeLineTestimonialTranslate> eTimeLines = new List<MTimeLineTestimonialTranslate>();

                eTimeLine.TimeLineTestId = id.ToInt32();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eTimeLines = new WebApiTimeLineTestimonialTranslate().GetTimeLineTestimonialTranslates(eTimeLine, objSession);


                objResult.data = eTimeLines.Select(x => new MTimeLineTestimonialTranslate
                {
                    TimeLineTestId = x.TimeLineTestId,
                    LanguageId = x.LanguageId,
                    Language = x.Language,
                    Testimonial = x.Testimonial
                }).ToList();


                //  objResult.data = eTimeLines;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "TimeLine");
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
                MTimeLineTestimonialTranslate objEnt = new MTimeLineTestimonialTranslate();
                objEnt.TimeLineTestId = Convert.ToInt32(id);
                objEnt.LanguageId = Convert.ToInt32(LanguageId);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiTimeLineTestimonialTranslate().DeleteTimeLineTestimonialTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Time Line Translate"); ;
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