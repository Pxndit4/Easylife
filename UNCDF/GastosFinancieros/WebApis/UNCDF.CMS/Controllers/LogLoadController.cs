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
    public class LogLoadController : Controller
    {
        public ActionResult Index()
        {
            List<SelectListItem> listDefault = new List<SelectListItem>();
            SelectListItem valDefault = new SelectListItem();
            valDefault.Value = "-1";
            valDefault.Text = "Select";
            listDefault.Add(valDefault);

            ViewBag.Loads = (Extension.GetLoads().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Value
            }).Union(listDefault)).OrderBy(z => (z.Value).ToInt32());

            return View();
        }

        [HttpPost]
        public JsonResult Search(SearchLogLoadViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MLogLoad ent = new MLogLoad();
                List<MLogLoad> listLog = new List<MLogLoad>();

                ent.TypeParamId = Convert.ToInt32(model.TypeParamId.ToString());
                if (string.IsNullOrEmpty(model.StartDate))
                {
                    ent.StartDate = 0;
                }
                else
                {
                    ent.StartDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.StartDate)), CultureInfo.InvariantCulture);
                }

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    ent.EndDate = 0;
                }
                else
                {
                    ent.EndDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.EndDate)), CultureInfo.InvariantCulture);
                }


                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                listLog = new WebApiLogLoad().GetLogsLoad(ent, objSession).Select(x => new MLogLoad
                {
                    LogloadId = x.LogloadId,
                    NameUser = x.NameUser,
                    DescriptionParam = x.DescriptionParam,
                    LoadingDate = x.LoadingDate,
                    TypeParamId = x.TypeParamId,
                    Total = x.Total,
                    TotalBadRecords = x.TotalBadRecords,
                    TotalCorrectRecords = x.TotalCorrectRecords
                }).ToList();

                objResult.data = listLog;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Project");
            }

            return Json(objResult);
        }

    }
}
