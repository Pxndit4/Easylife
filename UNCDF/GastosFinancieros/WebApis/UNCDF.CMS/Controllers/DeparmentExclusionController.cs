using UNCDF.CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;


namespace UNCDF.CMS.Controllers
{
    public class DeparmentExclusionController : Controller
    {
        // GET: DeparmentExclusion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddDeparmentCode(ProjectViewModel model)
        {

            ViewBag.Title = "Deparments";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Assignment");


            return View("AddDeparmentCode", new DeparmentViewModel { });
        }

        [HttpPost]
        public JsonResult SearchDeparmentCodeExclusions(ProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<MDeparmentExclusion> entList = new List<MDeparmentExclusion>();
                MDeparmentExclusion proj = new MDeparmentExclusion();

                entList = new WebApiProjectExclusions().ListDeparmentCodeExclusions(proj);

                objResult.data = entList.Select(x => new MDeparmentExclusion
                {
                    DeparmentCode = "8" + x.DeparmentCode
                }).ToList();

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Deparment");
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult DeleteDeparmentCode(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MDeparmentExclusion objEnt = new MDeparmentExclusion();
                objEnt.DeparmentCode = id;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiProjectExclusions().DeleteDeparmentCode(objEnt, objSession); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = string.Format(MessageResource.RowDeleteOK, "Deparment"); ;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = MessageResource.ControllerDeleteExceptionMessage;
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult SearchDeparment(DeparmentViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<MDeparment> entList = new List<MDeparment>();
                MDeparment proj = new MDeparment();

                proj.Description = Extension.ToEmpty(model.Description);
                proj.DeparmentCode = Extension.ToEmpty(model.DeparmentCode);
                //proj.DeparmentCode = proj.DeparmentCode.Substring(1);
                entList = new WebApiDeparment().FilDeparmentExclusion(proj);

                objResult.data = entList.Select(x => new MDeparment
                {
                    DeparmentId = x.DeparmentId,
                    DeparmentCode = '8' + x.DeparmentCode,
                    Description = x.Description
                }).ToList();

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Project");
            }

            return Json(objResult);
        }
        [HttpPost]
        public async Task<ActionResult> RegisterDeparmentCode(DeparmentViewModel model, List<string> opc)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {

                string[] codes = model.CheckDeparmentCode.Split(',').ToArray();

                MDeparmentExclusion objEnt = new MDeparmentExclusion
                {
                    //ProjectId = model.ProjectId,
                    ListCode = codes
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };
                response = new WebApiProjectExclusions().InsertDeparmentExlusions(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = string.Format(MessageResource.SaveSuccess, "Deparments");

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : MessageResul;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;

                objResult.message = string.Format(MessageResource.SaveSuccess, "Deparments");
            }
            return Json(objResult);
        }





    }
}
