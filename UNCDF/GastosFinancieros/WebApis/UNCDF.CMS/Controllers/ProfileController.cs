using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = UNCDF.Layers.Model;
using UNCDF.CMS.Models;

namespace UNCDF.CMS.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(SearchProfileViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                //objResult.data = BN.NPerfiles.BuscarPerfil(
                //    new BE.EPerfiles
                //    {
                //        LoginConexion = AutenticationManager.GetUser().Usuario,
                //        NombreConexion = AutenticationManager.GetUser().NombreConexion,
                //        IdEmpresa = AutenticationManager.GetUser().IdEmpresa,
                //        Abreviatura = model.Codigo.ToEmpty(),
                //        Descripcion = model.Nombre.ToEmpty()
                //    }).Select(x => new BuscarPerfilesResultadoViewModel
                //    {
                //        IdPerfil = x.IdPerfil,
                //        Codigo = x.Abreviatura,
                //        Nombre = x.Descripcion
                //    }).ToList();

                BE.MProfile MProfile = new BE.MProfile();
                List<BE.MProfile> MProfiles = new List<BE.MProfile>();

                MProfile.Description = Extension.ToEmpty(model.Profile);

                MProfiles = new WebApiProfile().GetProfiles(MProfile);

                objResult.data = MProfiles;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Profile");
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
                BE.MProfile objEnt = new BE.MProfile();
                objEnt.ProfileId = Convert.ToInt32(id);

                response = new WebApiProfile().DeletMProfile(objEnt); //Falta crear el metodo de editar

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


        public ActionResult New()
        {
            //BE.EPerfiles objResult;
            ViewBag.Title = "Register Profile";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Profile");

            ProfileViewModel ViewProfile = new ProfileViewModel();
            try
            {
                BE.Session objSession = new BE.Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                List<ResulOptionProfileViewModel> ListOptionProfile = new List<ResulOptionProfileViewModel>();

                ViewProfile.ProfileId = 0;
                ViewProfile.Profile = "";
                ViewProfile.Status = "1";

                List<BE.MOptions> options = new List<BE.MOptions>();
                BE.MOptions option = new BE.MOptions();
                option.ProfileId = 0;

                options = new WebApiOptions().GetOptionsByProfile(option, objSession);

                ResulOptionProfileViewModel oOptionProfile = new ResulOptionProfileViewModel();

                foreach (BE.MOptions item in options)
                {
                    oOptionProfile = new ResulOptionProfileViewModel();
                    oOptionProfile.ProfileId = item.ProfileId;
                    oOptionProfile.OptionId = item.OptionId;
                    oOptionProfile.FlagActive = item.FlagActive;
                    oOptionProfile.Title = item.Title;
                    oOptionProfile.TitleSubModule = item.TitleSubModule;
                    oOptionProfile.TitleModule = item.TitleModule;

                    if (item.FlagActive.ToString().Equals("1"))
                    {
                        oOptionProfile.FlagCheck = "true";
                    }
                    else { oOptionProfile.FlagCheck = "false"; }

                    ListOptionProfile.Add(oOptionProfile);
                }

                ViewProfile.Result = ListOptionProfile;
            }
            catch (Exception)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
            return PartialView("Register", ViewProfile);
        }

        public ActionResult Edit(string id)
        {
            BE.MProfile MProfile = new BE.MProfile();
            ProfileViewModel ViewProfile = new ProfileViewModel();
            List<ResulOptionProfileViewModel> ListOptionProfile = new List<ResulOptionProfileViewModel>();

            ViewBag.Title = "Edit Perfile";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Profile");

            try
            {
                ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Value
                });

                MProfile.ProfileId = Convert.ToInt32(id);

                MProfile = new WebApiProfile().GetProfile(MProfile);

                if (MProfile.ProfileId != 0)
                {
                    ViewProfile.Status = MProfile.Status.ToString();
                    ViewProfile.Profile = MProfile.Description;
                    ViewProfile.ProfileId = MProfile.ProfileId;

                    List<BE.MOptions> options = new List<BE.MOptions>();
                    BE.MOptions option = new BE.MOptions();
                    option.ProfileId = MProfile.ProfileId;

                    BE.Session objSession = new BE.Session()
                    {
                        UserId = AutenticationManager.GetUser().IdUsuario,
                    };


                    options = new WebApiOptions().GetOptionsByProfile(option, objSession);

                    ResulOptionProfileViewModel oOptionProfile = new ResulOptionProfileViewModel();

                    foreach (BE.MOptions item in options)
                    {
                        oOptionProfile = new ResulOptionProfileViewModel();
                        oOptionProfile.ProfileId = item.ProfileId;
                        oOptionProfile.OptionId = item.OptionId;
                        oOptionProfile.FlagActive = item.FlagActive;
                        oOptionProfile.Title = item.Title;
                        oOptionProfile.TitleSubModule = item.TitleSubModule;
                        oOptionProfile.TitleModule = item.TitleModule;

                        if (item.FlagActive.ToString().Equals("1"))
                        {
                            oOptionProfile.FlagCheck = "true";
                        }
                        else { oOptionProfile.FlagCheck = "false"; }

                        ListOptionProfile.Add(oOptionProfile);
                    }
                }

                ViewProfile.Result = ListOptionProfile;

                return PartialView("Register", ViewProfile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }



        //[HttpPost]
        //public async Task<ActionResult> Register(ProfileViewModel model, List<string> opc)
        //{
        //    JSonResult objResult = new JSonResult();
        //    string response = string.Empty;

        //    BE.MProfile profile = new BE.MProfile();
        //    BE.MProfileOptions optionsEnt = new BE.MProfileOptions();

        //    try
        //    {
        //        profile.ProfileId = model.ProfileId;
        //        profile.Description = model.Profile;
        //        profile.Options = new List<BE.MProfileOptions>();
        //        profile.Status = Convert.ToInt32(model.Status);

        //        for (int i = 0; i < opc.Count; i++)
        //        {
        //            if (opc[i].Equals("on"))
        //            {
        //                optionsEnt = new BE.MProfileOptions();
        //                optionsEnt.OptionId = model.Result[i].OptionId;
        //                optionsEnt.ProfileId = profile.ProfileId;
        //                profile.Options.Add(optionsEnt);
        //            }
        //        }

        //        if (model.ProfileId == 0)
        //            response = new InvokeApi.WebApiProfile().InsertProfile(profile);
        //        else
        //            response = new InvokeApi.WebApiProfile().UpdatMProfile(profile);

        //        string statusCode = response.Split('|')[0];
        //        string statusMessage = response.Split('|')[1];

        //        string MessageResul = (model.ProfileId == 0) ? string.Format(MessageResource.SaveSuccess, "Profile") : string.Format(MessageResource.UpdateSuccess, "Profile");

        //        objResult.isError = statusCode.Equals("2") ? true : false;
        //        objResult.message = statusCode.Equals("2") ? statusMessage : MessageResul;
        //    }
        //    catch (Exception ex)
        //    {
        //        objResult.isError = true;
        //        objResult.data = null;
        //        if (model.ProfileId == 0)
        //            objResult.message = string.Format(MessageResource.SaveError, "Profile");
        //        else
        //            objResult.message = string.Format(MessageResource.UpdateError, "Profile");
        //    }
        //    return Json(objResult);
        //}

        public ActionResult EditUsers(string id)
        {
            BE.MProfile MProfile = new BE.MProfile();

            ViewBag.Title = "Assign Users to Profile";
            try
            {
                MProfile.ProfileId = Convert.ToInt32(id);
                MProfile = new WebApiProfile().GetProfile(MProfile);

                AddUserViewModel ViewProfile = new AddUserViewModel();
                ViewProfile.ProfileId = MProfile.ProfileId;
                ViewProfile.Description = MProfile.Description;
                ViewProfile.Status = MProfile.Status.ToString();

                return View("AddUser", ViewProfile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        public ActionResult SearchUserProfile(string id)
        {
            SearchUserProfileViewModel ViewPerfilUsuario = new SearchUserProfileViewModel();
            ViewPerfilUsuario.ProfileId = (id.ToInt32());
            return View("SearchUserProfile", ViewPerfilUsuario);
        }

        [HttpPost]
        public JsonResult SearchUserProfile(SearchUserProfileViewModel model)
        {
            JSonResult objResult = new JSonResult();
            //BE.EPerfilUsuarios objEntParam;
            try
            {
                BE.MProfileUser MProfile = new BE.MProfileUser();

                MProfile.ProfileId = model.ProfileId;
                MProfile.User = Extension.ToEmpty(model.User);
                MProfile.Name = Extension.ToEmpty(model.Name);

                objResult.data = new WebApiProfile().GetUsersUnAssigned(MProfile).Select(x => new ResultSearchUserProfileViewModelViewModel
                {
                    ProfileId = model.ProfileId,
                    UserId = x.UserId,
                    User = x.User,
                    Name = x.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Users");
            }
            return Json(objResult);
        }

        [HttpPost]
        public JsonResult ListUsers(AddUserViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                BE.MProfile MProfile = new BE.MProfile();
                MProfile.ProfileId = model.ProfileId;

                List<BE.MProfileUser> lUsers = new List<BE.MProfileUser>();
                lUsers = new WebApiProfile().GetUsersProfile(MProfile);

                objResult.data = (lUsers).Select(x => new ResultUserProfileViewModel
                {
                    ProfileId = x.ProfileId,
                    UserId = x.UserId,
                    User = x.User,
                    Name = x.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Profile");
            }

            return Json(objResult);
        }

        [HttpPost]
        public ActionResult RegisterUserProfile(string ProfileId, string UserId)
        {
            //BE.ERetorno objDbResult = null;
            JSonResult objResult = new JSonResult();
            try
            {
                BE.MProfileUser MProfileUsers = new BE.MProfileUser();

                MProfileUsers.ProfileId = ProfileId.ToInt32();
                MProfileUsers.UserId = UserId.ToInt32();

                string response = string.Empty;

                response = new WebApiProfile().RegisterUsersProfile(MProfileUsers);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = string.Format(MessageResource.SaveSuccess, "User Profile");

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
        public ActionResult DeleteUser(string ProfileId, string UserId)
        {
            //BE.ERetorno objDbResult = null;
            JSonResult objResult = new JSonResult();
            try
            {
                BE.MProfileUser MProfileUsers = new BE.MProfileUser();

                MProfileUsers.ProfileId = ProfileId.ToInt32();
                MProfileUsers.UserId = UserId.ToInt32();

                string response = string.Empty;

                response = new WebApiProfile().DeleteUsersProfile(MProfileUsers);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = string.Format(MessageResource.SaveSuccess, "User Profile");

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
