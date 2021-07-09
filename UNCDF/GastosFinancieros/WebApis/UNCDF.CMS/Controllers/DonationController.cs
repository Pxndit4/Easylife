using UNCDF.CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using UNCDF.Layers.Model;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;


namespace UNCDF.CMS.Controllers
{
    public class DonationController : Controller
    {

        public ActionResult ListDonations()
        {

            SearchDonationViewModel viewModel = new SearchDonationViewModel();

            List<SelectListItem> listDefault = new List<SelectListItem>();
            SelectListItem valDefault = new SelectListItem();
            valDefault.Value = "-1";
            valDefault.Text = "Select";
            listDefault.Add(valDefault);

            Session objSession = new Session()
            {
                UserId = AutenticationManager.GetUser().IdUsuario,
            };


            ViewBag.TypeDonation = Extension.GetTypeDonation().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            });

            return View("ListDonations", viewModel);
        }

      

        [HttpPost]
        public JsonResult SearchDonations(SearchDonationViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MDonation ent = new MDonation();
                List<MDonation> listEnt = new List<MDonation>();

                ent.DonorId = model.TypeDonation;
                ent.FirstName = Extension.ToEmpty(model.FirstName).Trim();
                ent.LastName = Extension.ToEmpty(model.LastName).Trim();
                if (string.IsNullOrEmpty(model.StartDate))
                {
                    ent.StartDate = 0;
                }
                else
                {
                    ent.StartDate = decimal.Parse((Extension.ToFormatDateYYYYMMDD(model.StartDate)), CultureInfo.InvariantCulture);
                }

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    ent.EndDate = 0;
                }
                else
                {
                    ent.EndDate = decimal.Parse((Extension.ToFormatDateYYYYMMDD(model.EndDate)), CultureInfo.InvariantCulture);
                }

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                listEnt = new WebApiDonation().ListDonation(ent, objSession);

                objResult.data = listEnt.Select(x => new MDonation
                {
                    DonationId = x.DonationId,
                    Name = x.FirstName + ' ' + x.LastName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Project = x.Project,
                    DateStr = Extension.ToFormatDateDDMMYYY(x.Date.ToString("00000000")),
                    Amount = x.Amount,
                    Status = 1
                }).ToList();

                //  objResult.data = listEnt;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Donation");
            }

            return Json(objResult);
        }

    }
}