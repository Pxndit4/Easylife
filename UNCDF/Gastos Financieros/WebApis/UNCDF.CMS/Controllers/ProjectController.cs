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
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(ProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {

                List<MProject> entList = new List<MProject>();
                MProject proj = new MProject();
                proj.StartDate = 0;
                proj.EndDate = 0;
                proj.Title = "";
                proj.Status = "";

                entList = new WebApiProject().GetProjects(proj);

                model.ProjectCode = model.ProjectCode ?? "";
                model.Description = model.Description ?? "";

                if (!model.ProjectCode.Equals(""))
                {
                    entList = entList.Where(p => p.ProjectCode == model.ProjectCode).ToList();
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
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Project");
            }

            return Json(objResult);
        }

        public ActionResult Load()
        {
            ViewBag.Title = "Projects - Load File";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Projects");
            Session["ListProjects"] = null;


            return View("Load", new LoadProjectsViewModel());
        }

        [HttpPost]
        public ActionResult LoadFile(LoadProjectsViewModel model, HttpPostedFileBase imageFile)
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
                    objResult.data = null;
                    objResult.message = string.Format("Error: Please check the template for this upload ", "Projects");
                    return Json(objResult);
                }

                if (dt.Rows.Count <= 0)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = string.Format("The uploaded file has no rows", "Projects");
                    return Json(objResult);
                }

                try
                {
                    var dtResultado = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
                    dt = dtResultado.CopyToDataTable();

                    List<ModelProjectResult> entlist = new List<ModelProjectResult>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelProjectResult ent = new ModelProjectResult();
                        ent.ProjectCode = Extension.ToEmpty(dt.Rows[i][0].ToString());//Convert.ToInt32(dt.Rows[i]["StudentId"]);
                        ent.Description = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        if (ent.ProjectCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.ProjectCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code column is required </td></tr> ";
                        }

                        if (ent.Description.Length > 255)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column must not must not exceed 255 characters </td></tr> ";
                        }

                        if (ent.Description.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column is required </td></tr> ";
                        }

                        if (ent.AlertMessage.Length > 0)
                        {
                            ent.AlertMessage = "<table>" + ent.AlertMessage + "</table>";
                            ent.WithAlert = "S";
                        }

                        entlist.Add(ent);
                    }

                    Session["ListProjects"] = entlist;
                    objResult.data = entlist;
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = null;
                    objResult.message = "Projects :" + "Format error, check records";
                    return Json(objResult);
                }

                objResult.isError = false;
                objResult.message = null; // string.Format(MessageResource.SaveSuccess, "Load File save"); 
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = "Error loading Projects";
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult SearchLoad()
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<ModelProjectResult> entList = new List<ModelProjectResult>();
                entList = (List<ModelProjectResult>)Session["ListProjects"];
                // Session["ListProjectFinancials"] = null;
                objResult.data = entList;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Projects");
            }

            return Json(objResult);
        }

        [HttpPost]
        public ActionResult Register(LoadProjectsViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                List<MProject> entList = new List<MProject>();
                List<ModelProjectResult> entListData = new List<ModelProjectResult>();
                entListData = (List<ModelProjectResult>)Session["ListProjects"];

                foreach (ModelProjectResult item in entListData)
                {
                    MProject mProject = new MProject();
                    mProject.ProjectCode = item.ProjectCode;
                    mProject.Description = item.Description;
                    entList.Add(mProject);
                }

                response = new WebApiProject().InsertProject(entList, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2");
                objResult.message = string.Format(MessageResource.SaveSuccess, "Projects"); ;
            }
            catch (Exception ex)
            {
                objResult.message = string.Format(MessageResource.SaveError + "Error :" + ex.Message, "Projects");
            }
            return Json(objResult);
        }
    }
}
