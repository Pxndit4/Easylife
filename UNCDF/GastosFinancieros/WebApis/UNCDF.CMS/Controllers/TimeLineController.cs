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
    public class TimeLineController : Controller
    {
        public ActionResult ProjectTimeLines(string id)
        {
            MProject objresult;
            ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
            {

                Value = x.Id,
                Text = x.Value
            });
            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objresult = new WebApiProject().GetProject(new MProject
                {
                    ProjectId = id.ToInt32()
                }, objSession);
                TimeLineViewModel ViewTimeLine = new TimeLineViewModel();
                ViewTimeLine.ProjectId = objresult.ProjectId;
                ViewTimeLine.TitleProject = objresult.Title;
                //ViewTimeLine.Advance = objresult.Advance;


                return View("ProjectTimeLines", ViewTimeLine);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }
        // GET: TimeLine
        public ActionResult Index(string id)
        {
            //MProject eProject;
            //try
            //{
            //    eProject = new WebApiProject().GetProject(new MProject
            //    {
            //        ProjectId = id.ToInt32()
            //    });

            return View("Index", "");
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
            //    return View("_ErrorView");
            //}
        }

        [HttpPost]
        public JsonResult Search(TimeLineViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MTimeLine eTimeLine = new MTimeLine();
                List<MTimeLine> eTimeLines = new List<MTimeLine>();

                eTimeLine.ProjectId = model.ProjectId;
                eTimeLine.Title = Extension.ToEmpty(model.Title).Trim();
                if (string.IsNullOrEmpty(model.StartDate))
                {
                    eTimeLine.StartDate = 0;
                }
                else
                {
                    eTimeLine.StartDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.StartDate)), CultureInfo.InvariantCulture);
                }

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    eTimeLine.EndDate = 0;
                }
                else
                {
                    eTimeLine.EndDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.EndDate)), CultureInfo.InvariantCulture);
                }

                //eTimeLine.StartDate = decimal.Zero;
                //eTimeLine.EndDate = decimal.Zero;
                eTimeLine.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eTimeLines = new WebApiTimeLine().GetTimeLines(eTimeLine, objSession);


                objResult.data = eTimeLines.Select(x => new MTimeLine
                {
                    TimeLineId = x.TimeLineId,
                    ProjectId = x.ProjectId,
                    Title = x.Title,
                    Description = x.Description,
                    DateStr = Extension.ToFormatDateDDMMYYY(x.Date.ToString("00000000")),
                    Status = x.Status,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList().OrderByDescending(c => c.Status).ThenByDescending(n => n.Date);


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

        public ActionResult New(string id)
        {
            ViewBag.Title = "Register TimeLine";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "TimeLine");
            TimeLineViewModel viewModel = new TimeLineViewModel();
            MProject objProject;
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


                objProject = new WebApiProject().GetProject(new MProject
                {
                    ProjectId = id.ToInt32()
                }, objSession);

                viewModel.ProjectId = id.ToInt32();
                //viewModel.Advance = objProject.Advance;
                //viewModel.CurrentAdvance = objProject.Advance;
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
            MTimeLine objResult;
            MProject objProject;
            ViewBag.Title = "Edit TimeLine";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "TimeLine");
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

                var TimeLineId = id.Split('|')[0]; //TimeLineId
                var formCode = id.Split('|')[1];

                MTimeLine eTimeLine = new MTimeLine
                {
                    TimeLineId = Convert.ToInt32(TimeLineId)
                };

                objResult = new WebApiTimeLine().GetTimeLine(eTimeLine, objSession);

                objProject = new WebApiProject().GetProject(new MProject
                {
                    ProjectId = objResult.ProjectId
                }, objSession);

                ViewBag.ReturnForm = formCode;

                return View("Edit", new TimeLineViewModel()
                {
                    ProjectId = objResult.ProjectId,
                    TimeLineId = objResult.TimeLineId,
                    TitleProject = objProject.Title,
                    Title = objResult.Title,
                    Description = objResult.Description,
                    Date = Extension.ToFormatDateDDMMYYY(objResult.Date.ToString("00000000")),
                    //Advance = objProject.Advance,
                    //CurrentAdvance = objProject.Advance,
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
        public ActionResult GetTimeLine(string id)
        {
            JSonResult objResult = new JSonResult();
            //MTimeLine ent = new MTimeLine();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MTimeLine eTimeLine = new MTimeLine
                {
                    TimeLineId = Convert.ToInt32(id)
                };

                eTimeLine = new WebApiTimeLine().GetTimeLine(eTimeLine, objSession);

                //string statusCode = response.Split('|')[0];
                //string statusMessage = response.Split('|')[1];

                objResult.data = eTimeLine;
                //objResult.isError = statusCode.Equals("2") ? true : false;
                //objResult.message = string.Format(MessageResource.SaveSuccess, "TimeLine"); 

            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                //if (model.TimeLineId == 0)
                //    objResult.message = MessageResource.SaveError;
                //else
                //    objResult.message = MessageResource.UpdateError;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Register(TimeLineViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                MTimeLine objEnt = new MTimeLine
                {
                    ProjectId = model.ProjectId,
                    TimeLineId = model.TimeLineId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    Date = decimal.Parse((Extension.ToFormatDateYYYYMMDD(model.Date)), CultureInfo.InvariantCulture),
                    Advance = model.Advance,
                    Status = model.Status
                };



                if (model.TimeLineId == 0)
                    response = new WebApiTimeLine().InsertTimeLine(objEnt, objSession);
                else
                    response = new WebApiTimeLine().UpdateTimeLine(objEnt, objSession);

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
                MTimeLine objEnt = new MTimeLine();
                objEnt.TimeLineId = Convert.ToInt32(id);


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiTimeLine().DeleteTimeLine(objEnt, objSession); //Falta crear el metodo de editar

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
        public ActionResult UnApproved()
        {
            SearchTimeLineViewModel model = new SearchTimeLineViewModel();

            try
            {
                ViewBag.SubTitle = "TimeLine Aproved";
                model.Approved = 0;
                return View("UnApproved", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        public ActionResult UnApprovedRejected()
        {
            SearchTimeLineViewModel model = new SearchTimeLineViewModel();
            try
            {
                ViewBag.SubTitle = "TimeLine Rejected";
                model.Approved = 3;
                return View("UnApproved", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public JsonResult SearchUnApproved(string idapproved)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MTimeLine eTimeLine = new MTimeLine();
                List<MTimeLine> eTimeLines = new List<MTimeLine>();

                eTimeLine.Approved = Convert.ToInt32(idapproved);
                //eTimeLine.ProjectId = model.ProjectId;
                //eTimeLine.Title = Extension.ToEmpty(model.Title).Trim();
                //eTimeLine.StartDate = decimal.Zero;
                //eTimeLine.EndDate = decimal.Zero;
                //eTimeLine.Status = -1;


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eTimeLines = new WebApiTimeLine().GetTimeLinesUnApproved(eTimeLine, objSession);


                objResult.data = eTimeLines.Select(x => new MTimeLine
                {
                    TimeLineId = x.TimeLineId,
                    ProjectId = x.ProjectId,
                    Title = x.Title,
                    TitleProject = x.TitleProject,
                    ReasonReject = x.ReasonReject,
                    //Description = x.Description,
                    DateStr = Extension.ToFormatDateDDMMYYY(x.Date.ToString("00000000"))
                    //Status = x.Status,
                    //StatusName = (x.Status == 1) ? "Active" : "Inactive"
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

        public ActionResult RejectModal(string id)
        {
            ViewBag.Title = "Reject TimeLine";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "TimeLine");
            TimeLineViewModel viewModel = new TimeLineViewModel();

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                viewModel.TimeLineId = id.ToInt32();

            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("RejectModal", viewModel);
        }



        public ActionResult EditUnApproved(string id)
        {
            MTimeLine objResult;
            MProject objProject;
            ViewBag.Title = "TimeLine";
            ViewBag.Confirm = string.Format("Do you want to Approve the timeline?");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MTimeLine eTimeLine = new MTimeLine
                {
                    TimeLineId = Convert.ToInt32(id)
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiTimeLine().GetTimeLine(eTimeLine, objSession);

                objProject = new WebApiProject().GetProject(new MProject
                {
                    ProjectId = objResult.ProjectId
                }, objSession);


                return View("EditUnApproved", new TimeLineViewModel()
                {
                    ProjectId = objResult.ProjectId,
                    TimeLineId = objResult.TimeLineId,
                    TitleProject = objProject.Title,
                    Title = objResult.Title,
                    Description = objResult.Description,
                    Date = Extension.ToFormatDateDDMMYYY(objResult.Date.ToString("00000000")),
                    //Advance = objProject.Advance,
                    //CurrentAdvance = objProject.Advance,
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
        public ActionResult Approved(TimeLineViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                MTimeLine objEnt = new MTimeLine
                {
                    ProjectId = model.ProjectId,
                    TimeLineId = model.TimeLineId,
                    Title = Extension.ToEmpty(model.Title).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    Date = decimal.Parse((Extension.ToFormatDateYYYYMMDD(model.Date)), CultureInfo.InvariantCulture),
                    Advance = model.Advance,
                    Status = model.Status
                };

                response = new WebApiTimeLine().Approved(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.ApproveSuccess, "TimeLine"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = MessageResource.ApproveError;
            }
            return Json(objResult);
        }
        [HttpPost]
        public ActionResult Reject(TimeLineViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                MTimeLine objEnt = new MTimeLine
                {
                    TimeLineId = model.TimeLineId,
                    ReasonReject = Extension.ToEmpty(model.ReasonReject)
                };

                response = new WebApiTimeLine().Reject(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format("The Timeline has been Rejected", "TimeLine"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = MessageResource.ApproveError;
            }
            return Json(objResult);
        }


    }
}