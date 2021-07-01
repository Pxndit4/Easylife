using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using UNCDF.CMS.Models;

namespace UNCDF.CMS.Controllers
{
    public class InterfaceController : Controller
    {
        // GET: Interface
        public ActionResult Index()
        {
            ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            });

            ViewBag.Type = Extension.GetTypeInterface().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            });

            return View();
        }

        [HttpPost]
        public JsonResult Search(SearchInterfaceViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MInterface eInterface = new MInterface();
                List<MInterface> eInterfaces = new List<MInterface>();


                eInterface.InterfaceName = Extension.ToEmpty(model.InterfaceName).Trim();
                eInterface.TypeId = model.TypeId;
                eInterface.Status = (model.Status).ToString();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eInterfaces = new WebApiInterface().GetInterfaces(eInterface, objSession);


                objResult.data = eInterfaces.Select(x => new MInterface
                {
                    InterfaceId = x.InterfaceId,
                    TypeId = x.TypeId,
                    Type = (x.TypeId == 2) ? "Web" : "App",
                    InterfaceName = x.InterfaceName,
                    ControlName = x.ControlName,
                    Description = x.Description,
                    Status = x.Status,
                    StatusName = (x.Status == "1") ? "Active" : "Inactive"
                }).ToList();


                //  objResult.data = eInterfaces;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Interface");
            }

            return Json(objResult);
        }



        public ActionResult New()
        {
            ViewBag.Title = "Register Interface";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Interface");

            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });


                ViewBag.Type = Extension.GetTypeInterface().Select(x => new SelectListItem
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
            return View("Register", new InterfaceViewModel() { Status = "1" });
        }

        public ActionResult Edit(string id)
        {
            MInterface objResult;
            ViewBag.Title = "Edit Interface";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Interface");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                ViewBag.Type = Extension.GetTypeInterface().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MInterface eInterface = new MInterface
                {
                    InterfaceId = Convert.ToInt32(id)
                };


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objResult = new WebApiInterface().GetInterface(eInterface, objSession);

                return View("Register", new InterfaceViewModel()
                {
                    InterfaceId = objResult.InterfaceId,
                    Description = objResult.Description,
                    InterfaceName = objResult.InterfaceName,
                    TypeId = objResult.TypeId,
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
        public ActionResult Register(InterfaceViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MInterface objEnt = new MInterface
                {
                    InterfaceId = model.InterfaceId,
                    Description = Extension.ToEmpty(model.Description).Trim(),
                    InterfaceName = Extension.ToEmpty(model.InterfaceName).Trim(),
                    TypeId = model.TypeId,
                    Status = model.Status
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                if (model.InterfaceId == 0)
                    response = new WebApiInterface().InsertInterface(objEnt, objSession);
                else
                    response = new WebApiInterface().UpdateInterface(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.SaveSuccess, "Interface"); ;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.InterfaceId == 0)
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
                MInterface objEnt = new MInterface();
                objEnt.InterfaceId = Convert.ToInt32(id);

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                response = new WebApiInterface().DeleteInterface(objEnt, objSession); //Falta crear el metodo de editar

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

        public ActionResult InterfaceControls(string id)
        {
            MInterface objresult;

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                objresult = new WebApiInterface().GetInterface(new MInterface
                {
                    InterfaceId = id.ToInt32()
                }, objSession);
                InterfaceControlViewModel ViewInterfaceControl = new InterfaceControlViewModel();
                ViewInterfaceControl.InterfaceId = objresult.InterfaceId;
                ViewInterfaceControl.InterfaceName = objresult.InterfaceName;
                ViewInterfaceControl.Type = (objresult.TypeId == 2) ? "Web" : "App";

                return View("InterfaceControls", ViewInterfaceControl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }
    }
}