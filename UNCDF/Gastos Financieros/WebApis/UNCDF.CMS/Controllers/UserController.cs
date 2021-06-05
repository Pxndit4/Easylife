using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.CMS.Models;
using BE = UNCDF.Layers.Model;

namespace UNCDF.CMS.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Search(SearchUserViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                BE.MUser MUser = new BE.MUser();
                List<BE.MUser> MUsers = new List<BE.MUser>();

                MUser.User = Extension.ToEmpty(model.User).Trim();
                MUser.Name = Extension.ToEmpty(model.UserName).Trim();

                MUsers = new WebApiUser().GetUsers(MUser);

                objResult.data = MUsers;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "User");
            }

            return Json(objResult);
        }

        public ActionResult New()
        {
            ViewBag.Title = "Register User";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "User");

            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
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
            return View("Register", new UserViewModel() { });
        }


        [HttpPost]
        public JsonResult ChangePassword(string id)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                BE.MUser objEnt = new BE.MUser();
                objEnt.UserId = Convert.ToInt32(id);

                response = new WebApiUser().ChangePassword(objEnt); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : string.Format("Change password successful", "User"); ;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = MessageResource.ControllerDeleteExceptionMessage;
            }

            return Json(objResult);
        }


        public ActionResult Edit(string id)
        {
            BE.MUser objResult;
            ViewBag.Title = "Edit User";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "user");
            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                BE.MUser MUser = new BE.MUser
                {
                    UserId = Convert.ToInt32(id)
                };

                objResult = new WebApiUser().GetUser(MUser);

                return View("Register", new UserViewModel()
                {
                    UserId = objResult.UserId,
                    User = objResult.User,
                    Name = objResult.Name,
                    Type = objResult.Type,
                    Status = objResult.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                BE.MUser objEnt = new BE.MUser
                {
                    UserId = model.UserId,
                    User = model.User.Trim(),
                    Password = "",
                    Name = model.Name.Trim(),
                    Type = 2, //USUARIO CMS
                    Status = Convert.ToInt32(model.Status)
                };

                if (model.UserId == 0)
                    response = new WebApiUser().InsertUser(objEnt);
                else
                    response = new WebApiUser().UpdatetUser(objEnt); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = (model.UserId == 0) ? string.Format(MessageResource.SaveSuccess, "User") : string.Format(MessageResource.UpdateSuccess, "User");

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : MessageResul;
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                if (model.UserId == 0)
                    objResult.message = string.Format(MessageResource.SaveError, "User");
                else
                    objResult.message = string.Format(MessageResource.UpdateError, "User");
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
                BE.MUser objEnt = new BE.MUser();
                objEnt.UserId = Convert.ToInt32(id);

                response = new WebApiUser().DeletMUser(objEnt); //Falta crear el metodo de editar

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2") ? true : false;
                objResult.message = statusCode.Equals("2") ? statusMessage : string.Format(MessageResource.RowDeleteOK, "User"); ;
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