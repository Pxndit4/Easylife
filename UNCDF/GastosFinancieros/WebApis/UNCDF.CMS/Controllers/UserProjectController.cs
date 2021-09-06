using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using UNCDF.CMS.Models;


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
            SearchUserProfileViewModel ViewPerfilUsuario = new SearchUserProfileViewModel();
            ViewPerfilUsuario.ProfileId = (id.ToInt32());
            return View("SearchUserProfile", ViewPerfilUsuario);
        }

        //[HttpPost]
        //public JsonResult SearchUserProfile(SearchUserProfileViewModel model)
        //{
        //    JSonResult objResult = new JSonResult();
        //    //BE.EPerfilUsuarios objEntParam;
        //    try
        //    {
        //        BE.MProfileUser MProfile = new BE.MProfileUser();

        //        MProfile.ProfileId = model.ProfileId;
        //        MProfile.User = Extension.ToEmpty(model.User);
        //        MProfile.Name = Extension.ToEmpty(model.Name);

        //        objResult.data = new WebApiProfile().GetUsersUnAssigned(MProfile).Select(x => new ResultSearchUserProfileViewModelViewModel
        //        {
        //            ProfileId = model.ProfileId,
        //            UserId = x.UserId,
        //            User = x.User,
        //            Name = x.Name
        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        objResult.data = null;
        //        objResult.isError = true;
        //        objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Users");
        //    }
        //    return Json(objResult);
        //}

    }
}
