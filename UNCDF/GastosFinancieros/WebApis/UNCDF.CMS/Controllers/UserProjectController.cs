using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using UNCDF.CMS.Models;
using System.Globalization;

namespace UNCDF.CMS.Controllers
{
    public class UserProjectController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            });

            return View();
        }


        [HttpPost]
        public JsonResult Search(SearchUserProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MUserProject eInterface = new MUserProject();
                List<MUserProject> eInterfaces = new List<MUserProject>();


                eInterface.User = Extension.ToEmpty(model.User).Trim();
                eInterface.Name = Extension.ToEmpty(model.Name).Trim();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eInterfaces = new WebApiUserProject().GetUserProjectList(eInterface);


                objResult.data = eInterfaces;


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


        public ActionResult EditProjects(string id)
        {
            MUser user= new MUser();

            ViewBag.Title = "Assign Projects to User";
            try
            {
                user.UserId = Convert.ToInt32(id);
                user = new WebApiUser().GetUser(user);

                AddUserProjectViewModel ViewProfile = new AddUserProjectViewModel();
                ViewProfile.UserId = user.UserId;
                ViewProfile.User = user.User;
                ViewProfile.Name = user.Name;

                return View("AddProjectsUsser", ViewProfile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }


        [HttpPost]
        public JsonResult ListProjects(AddUserProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MUserProject MProfile = new MUserProject();
                MProfile.UserId = model.UserId;

                List<MUserProject> lUsersProject = new List<MUserProject>();
                lUsersProject = new WebApiUserProject().GetAssignedList(MProfile);

                objResult.data = (lUsersProject).Select(x => new ResultUserProjectViewModel
                {                    
                    UserId = x.UserId,
                    ProjectId = x.ProjectId,
                    ProjectCode = x.ProjectCode,
                    Title = x.Title
                }).ToList();
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "User Project");
            }

            return Json(objResult);
        }




        public ActionResult SearchUserProjectAdd(string id)
        {
            ProjectAddViewModel ViewPerfilUsuario = new ProjectAddViewModel();
            ViewPerfilUsuario.UserId = (id.ToInt32());
            return View("SearchProjectAdd", ViewPerfilUsuario);
        }


        [HttpPost]
        public JsonResult SearchProjectAdd(ProjectAddViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<MProject> entList = new List<MProject>();
                MProject proj = new MProject();
                proj.UserId = model.UserId;
                
                proj.StartDate = 0;
                proj.EndDate = 0;
                proj.Title = Extension.ToEmpty(model.Title);
                proj.EffectiveStatus = "-1";// String.Empty;//Extension.ToEmpty(model.EffectiveStatus);
                proj.ProjectCode = Extension.ToEmpty(model.ProjectCode);
                //proj.EffectiveStatus = "";

                entList = new WebApiUserProject().GetProjectNotAssignedList(proj);

                objResult.data = entList.Select(x => new MProject
                {
                    UserId = model.UserId,
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
        public ActionResult RegisterUserProject(string ProjectId, string UserId)
        {
            //BE.ERetorno objDbResult = null;
            JSonResult objResult = new JSonResult();
            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MUserProject MProfileUsers = new MUserProject();

                MProfileUsers.ProjectId = ProjectId.ToInt32();
                MProfileUsers.UserId = UserId.ToInt32();

                string response = string.Empty;


                response = new WebApiUserProject().RegisterUserProject(MProfileUsers, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = string.Format(MessageResource.SaveSuccess, "User Project");

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : MessageResul;
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
        public ActionResult DeleteProject(string ProjectId, string UserId)
        {
            //BE.ERetorno objDbResult = null;
            JSonResult objResult = new JSonResult();
            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MUserProject MProfileUsers = new MUserProject();

                MProfileUsers.ProjectId = ProjectId.ToInt32();
                MProfileUsers.UserId = UserId.ToInt32();

                string response = string.Empty;

                response = new WebApiUserProject().DeleteUserProject(MProfileUsers, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = string.Format(MessageResource.SaveSuccess, "User Project");

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : MessageResul;
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
