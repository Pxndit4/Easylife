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
    public class SubscribersController : Controller
    {
        // GET: Subscribers
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Search(SearchSubscribersViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MSubscribers eSubscriber = new MSubscribers();
                List<MSubscribers> eSubscribers = new List<MSubscribers>();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                eSubscriber.Email = Extension.ToEmpty(model.Email).Trim();

                eSubscribers = new WebApiSubscribers().GetSubscribers(eSubscriber, objSession);
                objResult.data = eSubscribers.Select(x => new MSubscribers
                {
                    Id = x.Id,
                    Email = x.Email
                }).ToList();

                //objResult.data = eBanners;
            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Banner");
            }

            return Json(objResult);
        }
        [HttpPost]
        public JsonResult SearchExport(SearchSubscribersViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MSubscribers eSubscriber = new MSubscribers();
                List<MSubscribers> eSubscribers = new List<MSubscribers>();

                eSubscriber.Email = Extension.ToEmpty(model.Email).Trim();


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                eSubscribers = new WebApiSubscribers().GetSubscribers(eSubscriber, objSession);
                

                //objResult.data = eDonors;

                FileManager.GenerateExcel(eSubscribers, "Subscribers");

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
            byte[] fileBytes = System.IO.File.ReadAllBytes(basePath + "Subscribers.xlsx");
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename= Subscribers.xlsx");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            return new FileStreamResult(Response.OutputStream, "application/vnd.ms-excel");
        }

    }
}
