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

namespace UNCDF.CMS.Controllers
{
    public class DonorPartnerController : Controller
    {
        // GET: DonorPartner
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(DonorPartnerViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {

                List<MDonorPartner> entList = new List<MDonorPartner>();

                entList = new WebApiDonorPartner().GetDonorPartners();

                model.DonorCode = model.DonorCode ?? "";
                model.DonorName = model.DonorName ?? "";

                if (!model.DonorCode.Equals(""))
                {
                    entList = entList.Where(p => p.DonorCode.Contains(model.DonorCode)).ToList();
                }

                if (!model.DonorName.Equals(""))
                {
                    entList = entList.Where(p => p.DonorName.Contains(model.DonorName) ).ToList();
                }

                objResult.data = entList;

            }
            catch (Exception)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Fund");
            }

            return Json(objResult);
        }

        public ActionResult Load()
        {
            ViewBag.Title = "Donors - Load File";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Donors");
            Session["ListDonors"] = null;


            return View("Load", new LoadDonorPartnersViewModel());
        }

        [HttpPost]
        public ActionResult LoadFile(LoadDonorPartnersViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string fileName = string.Empty;
            string Ext = string.Empty;
            byte[] imgData = null;//; new byte[0];
            string path = string.Empty;
            string basePath;
            // basePath = "E:\\TFS_Fuentes\\UnitLite\\Fuentes CMS Net\\CMSWeb\\File";

            basePath = Server.MapPath("~/File");
            DataTable dt;

            var include = new[] { "A", "B", "D","E" };
            try
            {
                if (imageFile != null)
                {
                    fileName = imageFile.FileName;
                    Ext = Path.GetExtension(imageFile.FileName);
                    // imgData = Extension.FileToByteArray(imageFile);
                    path = string.Format("{0}\\{1}", basePath, imageFile.FileName);
                }
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                imageFile.SaveAs(path);

                try
                {
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(path, false))
                    {
                        //Read the first Sheet from Excel file.
                        Sheet sheet = doc.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == "UNCDF");
                        //Get the Worksheet instance.
                        Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                        //Fetch all the rows present in the Worksheet.
                        IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                        dt = new DataTable();
                        //Loop through the Worksheet rows.
                        foreach (Row row in rows)
                        {
                            //Use the first row to add columns to DataTable.
                            if (row.RowIndex.Value == 2)
                            {
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    string cel = cell.CellReference;
                                    cel = cel.Substring(0, 1);
                                    if (include.Any(x => cel.Contains(x)))
                                    {//Continue adding the row to the table
                                        dt.Columns.Add(OpenXMLUtil.GetValue(doc, cell));
                                    }

                                }
                            }
                            else if (row.RowIndex.Value > 2)
                            {
                                //Add rows to DataTable.
                                dt.Rows.Add();
                                int i = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    string cel2 = cell.CellReference;
                                    cel2 = cel2.Substring(0, 1);
                                    if (include.Any(x => cel2.Contains(x)))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = OpenXMLUtil.GetValue(doc, cell);
                                        i++;
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = string.Format("Error: Please check the template for this upload ", "Funds");
                    return Json(objResult);
                }

                if (dt.Rows.Count <= 0)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = string.Format("The uploaded file has no rows", "Funds");
                    return Json(objResult);
                }

                try
                {
                    var dtResultado = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
                    dt = dtResultado.CopyToDataTable();

                    List<ModelDonorPartnerResult> entlist = new List<ModelDonorPartnerResult>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelDonorPartnerResult ent = new ModelDonorPartnerResult();
                        ent.DonorCode = Extension.ToEmpty(dt.Rows[i][0].ToString());//Convert.ToInt32(dt.Rows[i]["StudentId"]);
                        ent.DonorName = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.FundingPartner = Extension.ToEmpty(dt.Rows[i][2].ToString());
                        ent.DonorLongDescription = Extension.ToEmpty(dt.Rows[i][3].ToString());
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        if (ent.DonorCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Donor Code column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.DonorCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Donor Code column is required </td></tr> ";
                        }

                        if (ent.DonorName.Length > 255)
                        {
                            ent.AlertMessage += "<tr><td> - the Donor Name column must not must not exceed 255 characters </td></tr> ";
                        }

                        if (ent.DonorName.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Donor Name column is required </td></tr> ";
                        }

                        if (ent.FundingPartner.Length > 100)
                        {
                            ent.AlertMessage += "<tr><td> - the Funding Partner column must not must not exceed 100 characters </td></tr> ";
                        }

                        if (ent.AlertMessage.Length > 0)
                        {
                            ent.AlertMessage = "<table>" + ent.AlertMessage + "</table>";
                            ent.WithAlert = "S";
                        }

                        entlist.Add(ent);
                    }

                    Session["ListDonors"] = entlist;
                    objResult.data = entlist;
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = "Donors :" + "Format error, check records";
                    return Json(objResult);
                }

                objResult.isError = false;
                objResult.message = null; // string.Format(MessageResource.SaveSuccess, "Load File save"); 
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = "Error loading Donors";
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult SearchLoad()
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<ModelDonorPartnerResult> entList = new List<ModelDonorPartnerResult>();
                entList = (List<ModelDonorPartnerResult>)Session["ListDonors"];
                // Session["ListProjectFinancials"] = null;
                objResult.data = entList;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Donors");
            }

            return Json(objResult);
        }

        [HttpPost]
        public ActionResult Register(LoadFundsViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                List<MDonorPartner> entList = new List<MDonorPartner>();
                List<ModelDonorPartnerResult> entListData = new List<ModelDonorPartnerResult>();
                entListData = (List<ModelDonorPartnerResult>)Session["ListDonors"];

                foreach (ModelDonorPartnerResult item in entListData)
                {
                    MDonorPartner mDonorPartner = new MDonorPartner();
                    mDonorPartner.DonorCode = item.DonorCode;
                    mDonorPartner.DonorName = item.DonorName;
                    mDonorPartner.FundingPartner = item.FundingPartner;
                    mDonorPartner.DonorLongDescription = item.DonorLongDescription;
                    entList.Add(mDonorPartner);
                }

                response = new WebApiDonorPartner().InsertDonorPartner(entList, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2");
                objResult.message = statusCode.Equals("2") ? statusMessage : string.Format(MessageResource.SaveSuccess, "Donors"); ;
            }
            catch (Exception ex)
            {
                objResult.message = string.Format(MessageResource.SaveError + "Error :" + ex.Message, "Donors");
            }
            return Json(objResult);
        }
    }
}