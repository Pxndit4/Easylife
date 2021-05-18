using UNCDF.CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Controllers
{
    public class SeguridadController : Controller
    {
        private UserIdentity objUserIdenty = null;

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            MUser eUser = new MUser();
            MUser eUserReturn = new MUser();

            eUser.User = model.User;
            eUser.Password = model.Password;

            eUserReturn = new WebApiUser().LoginUser(eUser);

            if (eUserReturn == null || string.IsNullOrEmpty(eUserReturn.User))
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            else
            {

                if (eUserReturn.Status.Equals(0))
                {
                    ModelState.AddModelError("", "Inactive user");
                    return View(model);
                }
                else
                {
                    objUserIdenty = new UserIdentity();

                    objUserIdenty.Nombres = eUserReturn.Name;
                    objUserIdenty.IdUsuario = eUserReturn.UserId;
                    objUserIdenty.Usuario = eUserReturn.User;
                    objUserIdenty.Token = eUserReturn.Token;
                    AutenticationManager.SaveLogin(objUserIdenty);

                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        public ActionResult GetMenu(string id, PerfilViewModel model)
        {
            PanelViewModel modelObj = new PanelViewModel();
            List<MOptions> lstResult = null;
            List<MOptions> lstQuery;
            string strController = string.Empty;
            MOptions entOpciones = new MOptions();
            UserIdentity objUserIdenty = AutenticationManager.GetUser();

            Session objSession = new Session()
            {
                UserId = AutenticationManager.GetUser().IdUsuario,
            };

            entOpciones.ProfileId = Convert.ToInt32(id);
            lstResult = new WebApiOptions().GetOptions(entOpciones, objSession);

            AutenticationManager.SetOpciones(lstResult);
            AutenticationManager.SetPerfil(Convert.ToInt32(id));

            modelObj.PerfilId = id;

            lstQuery = lstResult.Where(x => x.Action == 1).ToList();

            modelObj.Items = new List<PanelTab>();

            foreach (var item in lstQuery)
            {
                PanelTab pnlTab = new PanelTab();
                pnlTab.Items = new List<PanelTabItem>();

                pnlTab.Id = "p" + item.OptionId.ToString();
                pnlTab.Text = item.Title;

                var lstN2 = lstResult.Where(a => a.Action == 2 && a.IdFather == item.OptionId);
                foreach (var itemN2 in lstN2)
                {
                    PanelTabItem pnlTabItem = new PanelTabItem()
                    {
                        Id = itemN2.OptionId.ToString(),
                        Text = itemN2.Title,
                        IsSeparator = true
                    };

                    pnlTab.Items.Add(pnlTabItem);

                    var lstN3 = lstResult.Where(a => a.Action == 3 && a.IdFather == itemN2.OptionId);
                    foreach (var itemN3 in lstN3)
                    {
                        pnlTab.Items.Add(new PanelTabItem()
                        {
                            Id = itemN3.OptionId.ToString(),
                            Text = itemN3.Title,
                            Action = string.IsNullOrEmpty(itemN3.Link) ? "" : itemN3.Link
                        });
                    }
                }

                modelObj.Items.Add(pnlTab);
            }

            return PartialView("_TabMenu", modelObj);
        }

        public ActionResult GetPerfil(int id)
        {
            List<PerfilViewModel> lstModel = new List<PerfilViewModel>();
            List<MProfile> lstResult = null;
            UserIdentity objUserIdenty = AutenticationManager.GetUser();


            MProfile entPerfiles = new MProfile();
            entPerfiles.UserId = Convert.ToInt32(AutenticationManager.GetUser().IdUsuario);

            lstResult = new WebApiProfile().GetProfilesByUser(entPerfiles);

            foreach (var item in lstResult)
            {
                lstModel.Add(new PerfilViewModel { Perfil = item.Description, Id = item.ProfileId.ToString() });
            };


            if ((objUserIdenty.IdEmpresa != id || objUserIdenty.IdPerfil == 0) && lstModel.Count > 0)
            {
                objUserIdenty.IdPerfil = Convert.ToInt32(lstModel.FirstOrDefault().Id);
            }

            // Guardo la empresa
            objUserIdenty.IdEmpresa = id;

            AutenticationManager.SaveLogin(objUserIdenty);
            return PartialView("_Perfil", lstModel);
        }

        private PerfilViewModel AdminPerfil()
        {
            return new PerfilViewModel
            {
                Id = ConfigurationManager.AppSettings["PERFIL_ID_ADMIN"],
                Perfil = ConfigurationManager.AppSettings["PERFIL_NO_ADMIN"]
            };

        }

        public ActionResult Logout()
        {
            AutenticationManager.Logout();
            Response.Redirect(Url.Action("Login", "Seguridad"), true);

            return View();
        }
    }
}