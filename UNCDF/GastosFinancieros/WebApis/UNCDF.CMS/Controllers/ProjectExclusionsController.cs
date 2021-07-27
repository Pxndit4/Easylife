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
    public class ProjectExclusionsController : Controller
    {
        // GET: ProjectExclusions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectExclusion()
        {
            return View("ProjectExclusion", new ProjectViewModel { EffectiveStatus = "Active" });
        }

        
        public ActionResult AddProjectCode(ProjectViewModel model)
        {

            ViewBag.Title = "Projects";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Assignment");


            return View("AddProjectCode", new ProjectViewModel { EffectiveStatus = "Active" });
        }

        [HttpPost]
        public JsonResult SearchProjects(ProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<MProject> entList = new List<MProject>();
                MProject proj = new MProject();
                if (string.IsNullOrEmpty(model.StartDate))
                {
                    proj.StartDate = 0;
                }
                else
                {
                    proj.StartDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.StartDate)), CultureInfo.InvariantCulture);
                }

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    proj.EndDate = 0;
                }
                else
                {
                    proj.EndDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.EndDate)), CultureInfo.InvariantCulture);
                }

                proj.Title = Extension.ToEmpty(model.Title);
                proj.EffectiveStatus = Extension.ToEmpty(model.EffectiveStatus);
                proj.ProjectCode = Extension.ToEmpty(model.ProjectCode);
                //proj.EffectiveStatus = "";

                entList = new WebApiProject().GetProjectsCodeExclusions(proj);

                objResult.data = entList.Select(x => new MProject
                {
                    ProjectId = x.ProjectId,
                    ProjectCode = x.ProjectCode,
                    Title = x.Title
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
        public async Task<ActionResult> RegisterProjectCode(ProjectViewModel model, List<string> opc)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {

                string[] codes = model.CheckProjectCode.Split(',').ToArray();

                MProjectExclusion objEnt = new MProjectExclusion
                {
                    //ProjectId = model.ProjectId,
                    ListCode = codes
                };

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };
                response = new WebApiProjectExclusions().InsertProjectExlusions(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = string.Format(MessageResource.SaveSuccess, "Profile");

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : MessageResul;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                
                objResult.message = string.Format(MessageResource.SaveSuccess, "Profile");
            }
            return Json(objResult);
        }
        [HttpPost]
        public JsonResult SearchProjectsCodeExclusions(ProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<MProjectExclusion> entList = new List<MProjectExclusion>();
                MProjectExclusion proj = new MProjectExclusion();
                
                entList = new WebApiProjectExclusions().ListProjectsCodeExclusions(proj);

                objResult.data = entList.Select(x => new MProjectExclusion
                {
                    ProjectCode = x.ProjectCode
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
        public JsonResult Delete(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                MProjectExclusion objEnt = new MProjectExclusion();
                objEnt.ProjectCode = id;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                response = new WebApiProjectExclusions().DeleteProjectCode(objEnt, objSession); //Falta crear el metodo de editar

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
