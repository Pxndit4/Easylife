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
using UNCDF.CMS.Util;

namespace UNCDF.CMS.Controllers
{
    public class DonorController : Controller
    {
        public ActionResult Index()
        {
            List<SelectListItem> listDefault = new List<SelectListItem>();
            SelectListItem valDefault = new SelectListItem();
            valDefault.Value = "-1";
            valDefault.Text = "Select";
            listDefault.Add(valDefault);

            Session objSession = new Session()
            {
                UserId = AutenticationManager.GetUser().IdUsuario,
            };

            var countries = new WebApiCountry().GetCountries(new MCountry
            {
                Description = string.Empty,
                Status = 1
            }, objSession).Select(x => new SelectListItem
            {
                Value = (x.CountryId).ToString(),
                Text = x.Description
            }).ToList();

            countries.Insert(0, new SelectListItem { Text = "   Select   ", Value = "-1", Selected = true });

            ViewBag.Countries = countries;


            ViewBag.Registered = (Extension.GetRegistered().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            }).Union(listDefault)).OrderBy(z => (z.Value).ToInt32());

            ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            });

            return View();
        }
        [HttpPost]
        public JsonResult Search(SearchDonorViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MDonor eDonor = new MDonor();
                List<MDonor> eDonors = new List<MDonor>();

                eDonor.FirstName = Extension.ToEmpty(model.FirstName).Trim();
                eDonor.LastName = Extension.ToEmpty(model.LastName).Trim();
                eDonor.CountryId = model.CountryId;
                eDonor.Registered = model.Registered;
                eDonor.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eDonors = new WebApiDonor().GetDonors(eDonor, objSession).Select(x => new MDonor
                {
                    DonorId = x.DonorId,
                    Name = (x.FirstName + " " + x.LastName).Trim(),
                    Email = x.Email,
                    Cellphone = x.Cellphone,
                    Country = x.Country,
                    Continent = x.Continent,
                    RegisteredName = (x.Registered == 1) ? "Yes" : "Not",
                    Address = x.Address,
                    //BirthdayStr = Extension.ToFormatDateDDMMYYY(x.Birthday.ToString()),
                    BirthdayStr = (x.Birthday == 0) ? "" : (Extension.ToFormatDateDDMMYYY(x.Birthday.ToString("00000000"))),
                    Status = x.Status,
                    Donated = x.Donated,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

                objResult.data = eDonors;

                // FileManager.GenerateExcel(eDonors, "Donor");

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
        public JsonResult SearchExport(SearchDonorViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MDonor eDonor = new MDonor();
                List<MDonor> eDonors = new List<MDonor>();

                eDonor.FirstName = Extension.ToEmpty(model.FirstName).Trim();
                eDonor.LastName = Extension.ToEmpty(model.LastName).Trim();
                eDonor.CountryId = model.CountryId;
                eDonor.Registered = model.Registered;
                eDonor.Status = model.Status;

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };
                eDonors = new WebApiDonor().GetDonors(eDonor, objSession).Select(x => new MDonor
                {
                    DonorId = x.DonorId,
                    Name = (x.FirstName + " " + x.LastName).Trim(),
                    Email = x.Email,
                    Cellphone = x.Cellphone,
                    Country = x.Country,
                    Continent = x.Continent,
                    RegisteredName = (x.Registered == 1) ? "Yes" : "Not",
                    Address = x.Address,
                    //BirthdayStr = Extension.ToFormatDateDDMMYYY(x.Birthday.ToString()),
                    BirthdayStr = (x.Birthday == 0) ? "" : (Extension.ToFormatDateDDMMYYY(x.Birthday.ToString("00000000"))),
                    Status = x.Status,
                    Donated = x.Donated,
                    StatusName = (x.Status == 1) ? "Active" : "Inactive"
                }).ToList();

                //objResult.data = eDonors;

                FileManager.GenerateExcel(eDonors, "Donor");

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Project");
            }

            return Json(objResult);
        }


        public FileResult ExportDownload()
        {

            string basePath;
            basePath = Server.MapPath("~/File/Exports/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(basePath + "Donor.xlsx");
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename= Donor.xlsx");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            return new FileStreamResult(Response.OutputStream, "application/vnd.ms-excel");
        }
    }
}
