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
    public class TimeLineTranslateController : Controller
    {
        // GET: TimeLineTranslate
        public ActionResult Index()
        {
            return View();
        }

        #region Translation
        public ActionResult NewTranslation(string id)
        {
            ViewBag.Title = "Register TimeLine Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "TimeLine Transalate");
            TimeLineTranslateViewModel viewModel = new TimeLineTranslateViewModel();
            MTimeLine objResult;


            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiTimeLine().GetTimeLine(new MTimeLine
                {
                    TimeLineId = id.ToInt32()
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

                viewModel.TimeLineId = id.ToInt32();
                viewModel.TitleProject = objResult.Title;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Register", viewModel);
        }

        public ActionResult Approve(string id)
        {
            ViewBag.Title = "TimeLine Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "TimeLine Transalate");
            TimeLineTranslateViewModel viewModel = new TimeLineTranslateViewModel();
            MTimeLine objResult;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiTimeLine().GetTimeLine(new MTimeLine
                {
                    TimeLineId = id.ToInt32()
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

                viewModel.TimeLineId = id.ToInt32();
                viewModel.TitleProject = objResult.Title;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("Approve", viewModel);
        }

        [HttpPost]
        public ActionResult Register(TimeLineTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            MTimeLineTranslate eTimeLineTransalate = new MTimeLineTranslate();

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MTimeLineTranslate objEnt = new MTimeLineTranslate
                {
                    TimeLineId = model.TimeLineId,
                    LanguageId = model.LanguageId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim()
                };

                eTimeLineTransalate = new WebApiTimeLineTranslate().GetTimeLineTranslate(new MTimeLineTranslate
                {
                    TimeLineId = model.TimeLineId,
                    LanguageId = model.LanguageId
                }, objSession);

                if (eTimeLineTransalate.LanguageId > 0)
                {
                    objResult.isError = true;
                    objResult.message = string.Format("This translation already exists for this language", "TimeLine Translate");
                    return Json(objResult);
                }


                response = new WebApiTimeLineTranslate().InsertTimeLineTranslate(objEnt, objSession);

                //    response = null;//new WebApiTimeLineTranslate().UpdateTimeLine(objEnt);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "TimeLine Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.TimeLineId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Update(TimeLineTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MTimeLineTranslate objEnt = new MTimeLineTranslate
                {
                    TimeLineId = model.TimeLineId,
                    LanguageId = model.LanguageId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim()
                };


                response = new WebApiTimeLineTranslate().UpdateTimeLineTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "TimeLine"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.TimeLineId == 0)
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
                MTimeLineTranslate eTimeLine = new MTimeLineTranslate();
                List<MTimeLineTranslate> eTimeLines = new List<MTimeLineTranslate>();

                eTimeLine.TimeLineId = id.ToInt32();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eTimeLines = new WebApiTimeLineTranslate().GetTimeLineTranslates(eTimeLine, objSession);


                objResult.data = eTimeLines.Select(x => new MTimeLineTranslate
                {
                    TimeLineId = x.TimeLineId,
                    LanguageId = x.LanguageId,
                    Language = x.Language,
                    Title = x.Title,
                    Description = x.Description
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
                MTimeLineTranslate objEnt = new MTimeLineTranslate();
                objEnt.TimeLineId = Convert.ToInt32(id);
                objEnt.LanguageId = Convert.ToInt32(LanguageId);


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                response = new WebApiTimeLineTranslate().DeleteTimeLineTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "TimeLine Translate"); ;
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