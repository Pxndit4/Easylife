using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using UNCDF.CMS.Models;

namespace UNCDF.CMS.Controllers
{
    public class InterfaceControlTranslateController : Controller
    {
        // GET: InterfaceControlTranslate
        public ActionResult Index()
        {
            return View();
        }

        #region Translation
        public ActionResult NewTranslation(string id)
        {
            ViewBag.Title = "Register Interface Control Translation";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Interface Control Transalate");
            InterfaceControlTranslateViewModel viewModel = new InterfaceControlTranslateViewModel();
            MInterfaceControl objResult;

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiInterfaceControl().GetInterfaceControl(new MInterfaceControl
                {
                    InterfaceControlId = id.ToInt32()
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

                viewModel.InterfaceControlId = id.ToInt32();
                viewModel.ControlName = objResult.ControlName;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return View("RegisterTranslate", viewModel);
        }

      

        [HttpPost]
        public ActionResult Register(InterfaceControlTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            MInterfaceControlTranslate eInterfaceControlTransalate = new MInterfaceControlTranslate();

            try
            {
                MInterfaceControlTranslate objEnt = new MInterfaceControlTranslate
                {
                    InterfaceControlId = model.InterfaceControlId,
                    LanguageId = model.LanguageId,
                    Description = Extension.ToEmpty(model.Description).Trim()
                };


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eInterfaceControlTransalate = new WebApiInterfaceControlTranslate().GetInterfaceControlTranslate(new MInterfaceControlTranslate
                {
                    InterfaceControlId = model.InterfaceControlId,
                    LanguageId = model.LanguageId
                }, objSession);

                if (!string.IsNullOrEmpty(eInterfaceControlTransalate.LanguageId))
                {
                    objResult.isError = true;
                    objResult.message = string.Format("This translation already exists for this language", "Interface Control Translate");
                    return Json(objResult);
                }


                response = new WebApiInterfaceControlTranslate().InsertInterfaceControlTranslate(objEnt, objSession);

                //    response = null;//new WebApiInterfaceControlTranslate().UpdateInterfaceControl(objEnt);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Interface Control Translate"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.InterfaceControlId == 0)
                    objResult.message = MessageResource.SaveConfirm;
                else
                    objResult.message = MessageResource.UpdateConfirm;
            }
            return Json(objResult);
        }


        [HttpPost]
        public ActionResult Update(InterfaceControlTranslateViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MInterfaceControlTranslate objEnt = new MInterfaceControlTranslate
                {
                    InterfaceControlId = model.InterfaceControlId,
                    LanguageId = model.LanguageId,
                    Description = Extension.ToEmpty(model.Description).Trim()
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiInterfaceControlTranslate().UpdateInterfaceControlTranslate(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "InterfaceControl"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.InterfaceControlId == 0)
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
                MInterfaceControlTranslate eInterfaceControl = new MInterfaceControlTranslate();
                List<MInterfaceControlTranslate> eInterfaceControls = new List<MInterfaceControlTranslate>();

                eInterfaceControl.InterfaceControlId = id.ToInt32();


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eInterfaceControls = new WebApiInterfaceControlTranslate().GetInterfaceControlTranslates(eInterfaceControl, objSession);


                objResult.data = eInterfaceControls.Select(x => new MInterfaceControlTranslate
                {
                    InterfaceControlId = x.InterfaceControlId,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    Description = x.Description
                }).ToList();


                //  objResult.data = eInterfaceControls;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "InterfaceControl");
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
                MInterfaceControlTranslate objEnt = new MInterfaceControlTranslate();
                objEnt.InterfaceControlId = Convert.ToInt32(id);
                objEnt.LanguageId = LanguageId;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiInterfaceControlTranslate().DeleteInterfaceControl(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Interface Control Translate"); ;
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