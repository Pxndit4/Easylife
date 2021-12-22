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
    public class FundController : Controller
    {
        // GET: Fund
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(FundViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {

                List<MFund> entList = new List<MFund>();

                entList = new WebApiFund().GetFunds();

                model.FundCode = model.FundCode ?? "";
                model.Description = model.Description ?? "";

                if (!model.FundCode.Equals(""))
                {
                    entList = entList.Where(p => p.FundCode == model.FundCode).ToList();
                }

                if (!model.Description.Equals(""))
                {
                    entList = entList.Where(p => p.Description == model.Description).ToList();
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
            ViewBag.Title = "Funds - Load File";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Funds");
            Session["ListFunds"] = null;


            return View("Load", new LoadFundsViewModel());
        }

        [HttpPost]
        public ActionResult LoadFile(LoadFundsViewModel model, HttpPostedFileBase imageFile)
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

            var include = new[] { "B", "C" };
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
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
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
                    objResult.data = new List<ModelFundResult>();
                    objResult.message = string.Format("Error: Please check the template for this upload ", "Funds");
                    return Json(objResult);
                }

                if (dt.Rows.Count <= 0)
                {
                    objResult.isError = true;
                    objResult.data = new List<ModelFundResult>();
                    objResult.message = string.Format("The uploaded file has no rows", "Funds");
                    return Json(objResult);
                }

                try
                {
                    var dtResultado = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
                    dt = dtResultado.CopyToDataTable();

                    List<ModelFundResult> entlist = new List<ModelFundResult>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelFundResult ent = new ModelFundResult();
                        ent.FundCode = Extension.ToEmpty(dt.Rows[i][0].ToString());//Convert.ToInt32(dt.Rows[i]["StudentId"]);
                        ent.Description = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        if (ent.FundCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Fund Code column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.FundCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Fund Code column is required </td></tr> ";
                        }

                        if (ent.Description.Length > 255)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column must not must not exceed 255 characters </td></tr> ";
                        }

                        if (ent.Description.Length == 0)
                        {
                            ent.AlertMessage +=  "<tr><td> - the Description column is required </td></tr> ";
                        }

                        if (ent.AlertMessage.Length > 0)
                        {
                            ent.AlertMessage = "<table>" + ent.AlertMessage + "</table>";
                            ent.WithAlert = "S";
                        }

                        entlist.Add(ent);
                    }

                    if (entlist != null)
                    {
                        var totalIncorrect = entlist.Where(x => x.AlertMessage.Length > 0).Count();
                        var total = entlist.Count();
                        var totalCorrect = total - totalIncorrect;

                        entlist = entlist.Select(w => {
                            w.Total = total;
                            w.TotalCorrectRecords = totalCorrect;
                            w.TotalBadRecords = totalIncorrect;
                            ; return w;
                        }).ToList();
                    }

                    Session["ListFunds"] = entlist;
                    objResult.data = entlist;
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = new List<ModelFundResult>();
                    objResult.message = "Funds :" + "Format error, check records";
                    return Json(objResult);
                }

                objResult.isError = false;
                objResult.message = null; // string.Format(MessageResource.SaveSuccess, "Load File save"); 
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = new List<ModelFundResult>();
                objResult.message = "Error loading Funds";
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult SearchLoad()
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<ModelFundResult> entList = new List<ModelFundResult>();
                entList = (List<ModelFundResult>)Session["ListFunds"];
                // Session["ListProjectFinancials"] = null;
                objResult.data = entList;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Funds");
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

                List<MFund> entList = new List<MFund>();
                List<ModelFundResult> entListData = new List<ModelFundResult>();
                entListData = (List<ModelFundResult>)Session["ListFunds"];

                var TotalCorrectRecords = 0;
                var TotalBadRecords = 0;

                foreach (ModelFundResult item in entListData)
                {
                    MFund mFund = new MFund();
                    mFund.FundCode = item.FundCode;
                    mFund.Description = item.Description;
                    entList.Add(mFund);

                    TotalCorrectRecords = item.TotalCorrectRecords;
                    TotalBadRecords = item.TotalBadRecords;
                }

                response = new WebApiFund().InsertFund(entList,TotalCorrectRecords, TotalBadRecords, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2");
                objResult.message = string.Format(MessageResource.SaveSuccess, "Funds"); ;
            }
            catch (Exception ex)
            {
                objResult.message = string.Format(MessageResource.SaveError + "Error :" + ex.Message, "Funds");
            }
            return Json(objResult);
        }
    }
}