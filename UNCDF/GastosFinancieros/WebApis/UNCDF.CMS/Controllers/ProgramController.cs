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
    public class ProgramController : Controller
    {
        // GET: Program
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(ProgramViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {

                List<MProgramName> entList = new List<MProgramName>();

                entList = new WebApiProgram().GetProgramNames();

                model.ProjectCode = model.ProjectCode ?? "";
                model.ProgramName = model.ProgramName ?? "";

                if (!model.ProjectCode.Equals(""))
                {
                    entList = entList.Where(p => p.ProjectCode.Contains(model.ProjectCode)).ToList();
                }

                if (!model.ProgramName.Equals(""))
                {
                    entList = entList.Where(p => p.ProgramName.Contains(model.ProgramName)).ToList();
                }

                objResult.data = entList;

            }
            catch (Exception)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Program Names");
            }

            return Json(objResult);
        }

        public ActionResult Load()
        {
            ViewBag.Title = "Program - Load File";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Program Names");
            Session["ListPrograms"] = null;


            return View("Load", new LoadProgamViewModel());
        }

        [HttpPost]
        public ActionResult LoadFile(LoadProgamViewModel model, HttpPostedFileBase imageFile)
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

            var include = new[] { "A", "C", "D", "E", "F", "G", "H" };
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

                    List<ModelProgramResult> entlist = new List<ModelProgramResult>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelProgramResult ent = new ModelProgramResult();
                        ent.ProjectCode = Extension.ToEmpty(dt.Rows[i][0].ToString());//Convert.ToInt32(dt.Rows[i]["StudentId"]);
                        ent.ProgramName = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.DonorCode = Extension.ToEmpty(dt.Rows[i][2].ToString());
                        ent.ProjectDetails = Extension.ToEmpty(dt.Rows[i][3].ToString());
                        ent.Sector = Extension.ToEmpty(dt.Rows[i][4].ToString());
                        ent.TaskManager = Extension.ToEmpty(dt.Rows[i][5].ToString());
                        ent.SDG = Extension.ToEmpty(dt.Rows[i][6].ToString());
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        //Project Code Validate
                        if (ent.ProjectCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.ProjectCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code column is required </td></tr> ";
                        }

                        //Program Name Validate
                        if (ent.ProgramName.Length > 255)
                        {
                            ent.AlertMessage += "<tr><td> - the Program Name column must not must not exceed 255 characters </td></tr> ";
                        }

                        if (ent.ProgramName.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Program Name column is required </td></tr> ";
                        }

                        //Donor validate
                        if (ent.DonorCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Donor Code column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.DonorCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Donor Code column is required </td></tr> ";
                        }

                        //Project Details validate
                        if (ent.ProjectDetails.Length > 4000)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Details column must not must not exceed 4000 characters </td></tr> ";
                        }

                        if (ent.ProjectDetails.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Details column is required </td></tr> ";
                        }

                        //Sector validate
                        if (ent.Sector.Length > 100)
                        {
                            ent.AlertMessage += "<tr><td> - the Sector column must not must not exceed 1oo characters </td></tr> ";
                        }

                        //TaskManager validate
                        if (ent.TaskManager.Length > 100)
                        {
                            ent.AlertMessage += "<tr><td> - the TaskManager column must not must not exceed 100 characters </td></tr> ";
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

                    Session["ListPrograms"] = entlist;
                    objResult.data = entlist;
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = "Funds :" + "Format error, check records";
                    return Json(objResult);
                }



                objResult.isError = false;
                objResult.message = null; // string.Format(MessageResource.SaveSuccess, "Load File save"); 
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
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
                List<ModelProgramResult> entList = new List<ModelProgramResult>();
                entList = (List<ModelProgramResult>)Session["ListPrograms"];
                // Session["ListProjectFinancials"] = null;
                objResult.data = entList;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Program Names");
            }

            return Json(objResult);
        }

        [HttpPost]
        public ActionResult Register(LoadProgamViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                List<MProgramName> entList = new List<MProgramName>();
                List<ModelProgramResult> entListData = new List<ModelProgramResult>();
                entListData = (List<ModelProgramResult>)Session["ListPrograms"];

                var TotalCorrectRecords = 0;
                var TotalBadRecords = 0;

                foreach (ModelProgramResult item in entListData)
                {
                    MProgramName mFund = new MProgramName();
                    mFund.ProjectCode = item.ProjectCode;
                    mFund.ProgramName = item.ProgramName;
                    mFund.DonorCode = item.DonorCode;
                    mFund.ProjectDetails = item.ProjectDetails;
                    mFund.TaskManager = item.TaskManager;
                    mFund.Sector = item.Sector;
                    mFund.SDG = item.SDG;
                    entList.Add(mFund);

                    TotalCorrectRecords = item.TotalCorrectRecords;
                    TotalBadRecords = item.TotalBadRecords;
                }

                response = new WebApiProgram().InsertProgramName(entList, TotalCorrectRecords, TotalBadRecords, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2");
                objResult.message = string.Format(MessageResource.SaveSuccess, "Program Names"); ;
            }
            catch (Exception ex)
            {
                objResult.message = string.Format(MessageResource.SaveError + "Error :" + ex.Message, "Program Names");
            }
            return Json(objResult);
        }
    }
}