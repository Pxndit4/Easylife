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
    public class ProjectFinancialController : Controller
    {
        
        // GET: ProjectFinancial
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(ProjectFinancialViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {
                MProjectFinancials proj = new MProjectFinancials();
                List<MProjectFinancials> entList = new List<MProjectFinancials>();

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };
                    
                proj.Year = Extension.ToEmpty(model.Year);

                entList = new WebApiProjectFinancial().GetProjectFinancials(proj);

                objResult.data = entList;

            }
            catch (Exception)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "ProjectFinancial");
            }

            return Json(objResult);
        }

        public ActionResult Load()
        {
            ViewBag.Title = "ProjectFinancials - Load File";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "ProjectFinancials");
            Session["ListProjectFinancials"] = null;


            return View("Load", new LoadProjectFinancialsViewModel());
        }

        [HttpPost]
        public ActionResult LoadFile(LoadProjectFinancialsViewModel model, HttpPostedFileBase imageFile)
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

            var include = new[] { "B", "C" , "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R" };
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
                        //Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        Sheets sheets = doc.WorkbookPart.Workbook.Sheets;
                        //Get the Worksheet instance.
                        dt = new DataTable();
                        int nSheet = 0;
                        //Loop through the Worksheet rows.
                        foreach (Sheet sheet in sheets)
                        {
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                            //Fetch all the rows present in the Worksheet.
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                            nSheet = nSheet+1;
                            foreach (Row row in rows)
                            {
                                //Use the first row to add columns to DataTable.
                                if (row.RowIndex.Value == 2 && nSheet==1)
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
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = string.Format("Error: Please check the template for this upload ", "ProjectFinancials");
                    return Json(objResult);
                }

                if (dt.Rows.Count <= 0)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = string.Format("The uploaded file has no rows", "ProjectFinancials");
                    return Json(objResult);
                }

                try
                {
                    var dtResultado = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
                    dt = dtResultado.CopyToDataTable();

                    List<ModelProjectFinancialResult> entlist = new List<ModelProjectFinancialResult>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelProjectFinancialResult ent = new ModelProjectFinancialResult();
                        ent.Year                    = Extension.ToEmpty(dt.Rows[i][0].ToString());
                        ent.OperUnit                = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.DeparmentCode           = Extension.ToEmpty(dt.Rows[i][2].ToString());
                        ent.ProjectCode             = Extension.ToEmpty(dt.Rows[i][3].ToString());
                        ent.DescrProject            = Extension.ToEmpty(dt.Rows[i][4].ToString());
                        ent.ProjectManager          = Extension.ToEmpty(dt.Rows[i][5].ToString());
                        ent.ImplementAgencyCode     = Extension.ToEmpty(dt.Rows[i][6].ToString());
                        ent.ShortDesc               = Extension.ToEmpty(dt.Rows[i][7].ToString());
                        ent.FundCode                = Extension.ToEmpty(dt.Rows[i][8].ToString());
                        ent.DescrFund               = Extension.ToEmpty(dt.Rows[i][9].ToString());
                        ent.Budget                  = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][10].ToString()));
                        ent.PreEncumbrance          = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][11].ToString()));
                        ent.Encumbrance             = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][12].ToString()));
                        ent.Disbursement            = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][13].ToString()));
                        ent.Expenditure             = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][14].ToString()));
                        ent.Balance                 = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][15].ToString()));
                        ent.Spent                   = Convert.ToDecimal(Extension.ToEmpty(dt.Rows[i][16].ToString()));
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        //if (ent.ProjectFinancialCode.Length > 10)
                        //{
                        //    ent.AlertMessage += "<tr><td> - the ProjectFinancial Code column must not must not exceed 10 characters </td></tr> ";
                        //}

                        //if (ent.ProjectFinancialCode.Length == 0)
                        //{
                        //    ent.AlertMessage += "<tr><td> - the ProjectFinancial Code column is required </td></tr> ";
                        //}

                        //if (ent.Description.Length > 255)
                        //{
                        //    ent.AlertMessage += "<tr><td> - the Description column must not must not exceed 255 characters </td></tr> ";
                        //}

                        //if (ent.Description.Length == 0)
                        //{
                        //    ent.AlertMessage += "<tr><td> - the Description column is required </td></tr> ";
                        //}

                        //if (ent.AlertMessage.Length > 0)
                        //{
                        //    ent.AlertMessage = "<table>" + ent.AlertMessage + "</table>";
                        //    ent.WithAlert = "S";
                        //}

                        entlist.Add(ent);
                    }

                    Session["ListProjectFinancials"] = entlist;
                    objResult.data = entlist;
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = "ProjectFinancials :" + "Format error, check records";
                    return Json(objResult);
                }

                objResult.isError = false;
                objResult.message = null; // string.Format(MessageResource.SaveSuccess, "Load File save"); 
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = "Error loading ProjectFinancials";
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult SearchLoad()
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<ModelProjectFinancialResult> entList = new List<ModelProjectFinancialResult>();
                entList = (List<ModelProjectFinancialResult>)Session["ListProjectFinancials"];
                // Session["ListProjectFinancials"] = null;
                objResult.data = entList;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "ProjectFinancials");
            }

            return Json(objResult);
        }

        [HttpPost]
        public ActionResult Register(LoadProjectFinancialsViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                List<MProjectFinancials> entList = new List<MProjectFinancials>();
                List<ModelProjectFinancialResult> entListData = new List<ModelProjectFinancialResult>();
                entListData = (List<ModelProjectFinancialResult>)Session["ListProjectFinancials"];

                foreach (ModelProjectFinancialResult item in entListData)
                {
                    MProjectFinancials mProjectFinancial = new MProjectFinancials();
                    mProjectFinancial.Year                =item.Year;
                    mProjectFinancial.OperUnit = item.OperUnit;
                    mProjectFinancial.DeparmentCode = item.DeparmentCode;
                    mProjectFinancial.ProjectCode = item.ProjectCode;
                    mProjectFinancial.DescrProject = item.DescrProject;
                    mProjectFinancial.ProjectManager = item.ProjectManager;
                    mProjectFinancial.ImplementAgencyCode = item.ImplementAgencyCode;
                    mProjectFinancial.ShortDesc = item.ShortDesc;
                    mProjectFinancial.FundCode = item.FundCode;
                    mProjectFinancial.DescrFund = item.DescrFund;
                    mProjectFinancial.Budget = item.Budget;
                    mProjectFinancial.PreEncumbrance = item.PreEncumbrance;
                    mProjectFinancial.Encumbrance = item.Encumbrance;
                    mProjectFinancial.Disbursement = item.Disbursement;
                    mProjectFinancial.Expenditure = item.Expenditure;
                    mProjectFinancial.Balance = item.Balance;
                    mProjectFinancial.Spent = item.Spent;
                    entList.Add(mProjectFinancial);
                }

                response = new WebApiProjectFinancial().InsertProjectFinancial(entList, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2");
                objResult.message = string.Format(MessageResource.SaveSuccess, "ProjectFinancials"); ;
            }
            catch (Exception ex)
            {
                objResult.message = string.Format(MessageResource.SaveError + "Error :" + ex.Message, "ProjectFinancials");
            }
            return Json(objResult);
        }
    }
    
}
