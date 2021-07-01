using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using UNCDF.CMS.Models;

namespace UNCDF.CMS.Controllers
{
    public class InterfaceControlController : Controller
    {
        // GET: InterfaceControl
        public ActionResult Index(string id)
        {
            MInterface eInterface;
            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eInterface = new WebApiInterface().GetInterface(new MInterface
                {
                    InterfaceId = id.ToInt32()
                }, objSession);

                return View("Index", "");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public JsonResult Search(InterfaceControlViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MInterfaceControl eInterfaceControl = new MInterfaceControl();
                List<MInterfaceControl> eInterfaceControls = new List<MInterfaceControl>();

                eInterfaceControl.InterfaceId = model.InterfaceId;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eInterfaceControls = new WebApiInterfaceControl().GetInterfaceControls(eInterfaceControl, objSession);


                objResult.data = eInterfaceControls.Select(x => new MInterfaceControl
                {
                    InterfaceControlId = x.InterfaceControlId,
                    InterfaceId = x.InterfaceId,
                    ControlName = x.ControlName,
                    Description = x.Description,
                    DescriptionControl = x.DescriptionControl
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

        public ActionResult New(string id)
        {
            ViewBag.Title = "Register Interface Control";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "InterfaceControl");
            InterfaceControlViewModel viewModel = new InterfaceControlViewModel();
            try
            {
                viewModel.InterfaceId = id.ToInt32();
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
            MInterfaceControl objResult;
            ViewBag.Title = "Edit InterfaceControl";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "InterfaceControl");
            try
            {

                MInterfaceControl eInterfaceControl = new MInterfaceControl
                {
                    InterfaceControlId = Convert.ToInt32(id)
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiInterfaceControl().GetInterfaceControl(eInterfaceControl, objSession);

                return View("Register", new InterfaceControlViewModel()
                {
                    InterfaceId = objResult.InterfaceId,
                    InterfaceControlId = objResult.InterfaceControlId,
                    Description = objResult.Description,
                    ControlName = objResult.ControlName,
                    DescriptionControl = objResult.DescriptionControl
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult Register(InterfaceControlViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MInterfaceControl objEnt = new MInterfaceControl
                {
                    InterfaceId = model.InterfaceId,
                    InterfaceControlId = model.InterfaceControlId,
                    ControlName = Extension.ToEmpty(model.ControlName).Trim(),
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    DescriptionControl = Extension.ToEmpty(model.DescriptionControl).Trim(),

                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                if (model.InterfaceControlId == 0)
                    response = new WebApiInterfaceControl().InsertInterfaceControl(objEnt, objSession);
                else
                    response = new WebApiInterfaceControl().UpdateInterfaceControl(objEnt, objSession);

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
        public JsonResult Delete(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MInterfaceControl objEnt = new MInterfaceControl();
                objEnt.InterfaceControlId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiInterfaceControl().DeleteInterfaceControl(objEnt, objSession); //Falta crear el metodo de editar

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