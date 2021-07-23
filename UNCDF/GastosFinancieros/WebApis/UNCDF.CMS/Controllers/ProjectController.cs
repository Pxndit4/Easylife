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
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            
                 
             var estados = Extension.GetStatus().Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Value
            }).ToList();

            estados.Insert(0, new SelectListItem { Text = "---Select---", Value = "-1", Selected = true });
            ViewBag.Estado  = estados;

            return View();
        }
        public ActionResult ProjectActives()
        {
            ViewBag.Estado = Extension.GetStatus().Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Value
            });

            return View("ProjectActives", new ProjectViewModel { EffectiveStatus = "Active"});
        }

        [HttpPost]
        public JsonResult Search(ProjectViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {

                List<MProject> entList = new List<MProject>();
                MProject proj = new MProject();
                if (string.IsNullOrEmpty(model.StartDate))
                {
                    proj.StartDate = 0;
                }
                else
                {
                    proj.StartDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.StartDate)), CultureInfo.InvariantCulture);
                }

                if (string.IsNullOrEmpty(model.EndDate))
                {
                    proj.EndDate = 0;
                }
                else
                {
                    proj.EndDate = Int32.Parse((Extension.ToFormatDateYYYYMMDD(model.EndDate)), CultureInfo.InvariantCulture);
                }

                proj.Title = Extension.ToEmpty(model.Title);
                proj.EffectiveStatus = Extension.ToEmpty(model.EffectiveStatus);
                proj.ProjectCode = Extension.ToEmpty(model.ProjectCode);
                //proj.EffectiveStatus = "";

                entList = new WebApiProject().GetProjects(proj);

                //model.ProjectCode = model.ProjectCode ?? "";
                //model.Title = model.Title ?? "";
                //model.EffectiveStatus = model.EffectiveStatus ?? "";

                //if (!model.ProjectCode.Equals(""))
                //{
                //    entList = entList.Where(p => p.ProjectCode == model.ProjectCode).ToList();
                //}

                //if (!model.Title.Equals(""))
                //{
                //    entList = entList.Where(p => p.Description == model.Title).ToList();
                //}

                //objResult.data = entList;
                objResult.data = entList.Select(x => new MProject
                {
                    ProjectId = x.ProjectId,
                    Department = x.Department,
                    ProjectCode = x.ProjectCode ,
                    Description = x.Description,
                    Type = x.Type,
                    EffectiveStatus = x.EffectiveStatus,
                    StatusEffDateStr = Extension.ToFormatDateDDMMYYY(x.StatusEffDate.ToString()),
                    StatusEffSeq = x.StatusEffSeq,
                    Status = x.Status,
                    StatusDescription = x.StatusDescription,
                    StartDateStr = Extension.ToFormatDateDDMMYYY(x.StartDate.ToString()),
                    EndDateStr = Extension.ToFormatDateDDMMYYY(x.EndDate.ToString()),
                    AwardId = x.AwardId,
                    Title = x.Title,
                    IsVisible = x.IsVisible,
                    Donation = x.Donation,
                    AwardStatus = x.AwardStatus
                }).ToList();

            }
            catch (Exception e)
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

            var include = new[] {  "B", "C","D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
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
                                        string val= OpenXMLUtil.GetValue(doc, cell);
                                        if (cel2.Contains("K") || cel2.Contains("L")|| cel2.Contains("G"))
                                        {
                                            try
                                            {
                                                DateTime date = DateTime.FromOADate(Convert.ToDouble(val));
                                                val = date.ToString("dd/MM/yyyy");
                                            }
                                            catch (Exception ex)
                                            {
                                                objResult.isError = true;
                                                objResult.message = string.Format("Error: Error in the date field please use format DD/MM/YYYY", "Project");
                                                return Json(objResult);
                                            }
                                        }
                                        dt.Rows[dt.Rows.Count - 1][i] = val;
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
                    
                    List<MProgramName> entVaList = new List<MProgramName>();
                    entVaList = new WebApiProgram().GetValidProgramNames();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelProjectResult ent = new ModelProjectResult();
                        ent.Department = Extension.ToEmpty(dt.Rows[i][0].ToString());
                        ent.ProjectCode = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.Description = Extension.ToEmpty(dt.Rows[i][2].ToString());
                        ent.Type = Extension.ToEmpty(dt.Rows[i][3].ToString());
                        ent.EffectiveStatus = Extension.ToEmpty(dt.Rows[i][4].ToString());
                        ent.StatusEffDateStr = Extension.ToEmpty(dt.Rows[i][5].ToString());
                        ent.StatusEffSeq = Extension.ToInt32(dt.Rows[i][6].ToString());
                        ent.Status = Extension.ToEmpty(dt.Rows[i][7].ToString());
                        ent.StatusDescription = Extension.ToEmpty(dt.Rows[i][8].ToString());
                        ent.StartDateStr = Extension.ToEmpty(dt.Rows[i][9].ToString());
                        ent.EndDateStr = Extension.ToEmpty(dt.Rows[i][10].ToString());
                        ent.AwardId = Extension.ToEmpty(dt.Rows[i][11].ToString());
                        ent.Title = Extension.ToEmpty(dt.Rows[i][12].ToString());
                        ent.AwardStatus = Extension.ToEmpty(dt.Rows[i][13].ToString());
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        if (ent.ProjectCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code column must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.ProjectCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code column is required </td></tr> ";
                        }

                        var valid = entVaList.Where(p => p.ProjectCode == ent.ProjectCode).FirstOrDefault();
                        if (valid != null)
                        {
                            if (string.IsNullOrEmpty(valid.DonorCode))
                            {
                                ent.AlertMessage += "<tr><td> - the Program Name has no Donor Partner </td></tr> ";
                            }
                        }
                        else
                        {
                            ent.AlertMessage += "<tr><td> - the Project Code does not exist in Program Name </td></tr> ";
                        }

                        if (ent.Department.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Department column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.Description.Length > 255)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column must not must not exceed 255 characters </td></tr> ";
                        }

                        if (ent.Description.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column is required </td></tr> ";
                        }

                        if (ent.Type.Length > 50)
                        {
                            ent.AlertMessage += "<tr><td> - the Type column must not must not exceed 50 characters </td></tr> ";
                        }

                        if (ent.Type.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Type column is required </td></tr> ";
                        }
                        if (ent.EffectiveStatus.Length > 20)
                        {
                            ent.AlertMessage += "<tr><td> - the Effective Status column must not must not exceed 20 characters </td></tr> ";
                        }
                        if (ent.StatusEffDateStr.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Effective Status column must not must not exceed 10 characters </td></tr> ";
                        }
                        if (ent.Status.Length > 20)
                        {
                            ent.AlertMessage += "<tr><td> - the  Status column must not must not exceed 20 characters </td></tr> ";
                        }
                        if (ent.Status.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Status column is required </td></tr> ";
                        }
                        if (ent.StatusDescription.Length > 50)
                        {
                            ent.AlertMessage += "<tr><td> - the  Status Description column must not must not exceed 20 characters </td></tr> ";
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
                    mProject.Department = item.Department;
                    mProject.ProjectCode = item.ProjectCode;
                    mProject.Description = item.Description;
                    mProject.Type = item.Type;
                    mProject.EffectiveStatus = item.EffectiveStatus;
                    mProject.StatusEffDate = Convert.ToInt32(Extension.ToFormatDateYYYYMMDD(item.StatusEffDateStr));
                    mProject.StatusEffSeq = item.StatusEffSeq;
                    mProject.Status = item.Status;
                    mProject.StatusDescription = item.StatusDescription;
                    mProject.StartDate =  Convert.ToInt32(Extension.ToFormatDateYYYYMMDD( item.StartDateStr));
                    mProject.EndDate = Convert.ToInt32(Extension.ToFormatDateYYYYMMDD(item.EndDateStr));//item.EndDate;
                    mProject.Title = item.Title;
                    mProject.AwardId = item.AwardId;
                    mProject.AwardStatus = item.AwardStatus;
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

        public ActionResult Edit(string id)
        {
            MProject objResult;
            ViewBag.Title = "Edit Project";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Project");
            string ProjectPath = ConfigurationManager.AppSettings["URLProject"].ToString();
            string viewName = "";

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MProject eProjects = new MProject
                {
                    ProjectId = Convert.ToInt32(id)
                };

                objResult = new WebApiProject().GetProject(eProjects, objSession);

                return View("Edit", new ProjectEditViewModel()
                {
                    ProjectId = objResult.ProjectId,
                    ProjectCode = objResult.ProjectCode,
                    Title = objResult.Title,
                    StartDateStr = Extension.ToFormatDateDDMMYYY(objResult.StartDate.ToString()),
                    EndDateStr = Extension.ToFormatDateDDMMYYY(objResult.EndDate.ToString()),
                    Image = (objResult.Image.Replace(Extension.S3Server, "")).Replace(ProjectPath, ""),
                    Video = (objResult.Video.Replace(Extension.S3Server, "")).Replace(ProjectPath, ""),
                    Description = Extension.ToEmpty(objResult.Description).Trim(),
                    Type = Extension.ToEmpty(objResult.Type).Trim(),
                    EffectiveStatus = Extension.ToEmpty(objResult.EffectiveStatus).Trim(),
                    StatusEffDateStr = Extension.ToFormatDateDDMMYYY(objResult.StatusEffDate.ToString()),
                    StatusEffSeq = objResult.StatusEffSeq,
                    IsVisible = objResult.IsVisible,
                    Donation = objResult.Donation,
                    StatusDescription = Extension.ToEmpty(objResult.StatusDescription).Trim(),
                    AwardId = Extension.ToEmpty(objResult.AwardId).Trim(),
                    AwardStatus = Extension.ToEmpty(objResult.AwardStatus).Trim(),
                    ImageLink = (objResult.Image == String.Empty || objResult.Image == null) ? "" : Extension.S3Server + objResult.Image,
                    VideoLink = (objResult.Video == String.Empty || objResult.Video == null) ? "" : Extension.S3Server + objResult.Video,
                    Status = objResult.Status
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        public ActionResult GetRead(string id)
        {
            MProject objResult;
            ViewBag.Title = "Project Details";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Project");
            string ProjectPath = ConfigurationManager.AppSettings["URLProject"].ToString();

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MProject eProjects = new MProject
                {
                    ProjectId = Convert.ToInt32(id)
                };

                objResult = new WebApiProject().GetProject(eProjects, objSession);

                return View("RegisterReadOnly", new ProjectEditViewModel()
                {
                    ProjectId = objResult.ProjectId,
                    ProjectCode = objResult.ProjectCode,
                    Title = objResult.Title,
                    StartDateStr = Extension.ToFormatDateDDMMYYY(objResult.StartDate.ToString()),
                    EndDateStr = Extension.ToFormatDateDDMMYYY(objResult.EndDate.ToString()),
                    Image = (objResult.Image.Replace(Extension.S3Server, "")).Replace(ProjectPath, ""),
                    Video = (objResult.Video.Replace(Extension.S3Server, "")).Replace(ProjectPath, ""),
                    Description = Extension.ToEmpty(objResult.Description).Trim(),
                    Type = Extension.ToEmpty(objResult.Type).Trim(),
                    EffectiveStatus = Extension.ToEmpty(objResult.EffectiveStatus).Trim(),
                    StatusEffDateStr = Extension.ToFormatDateDDMMYYY(objResult.StatusEffDate.ToString()),
                    StatusEffSeq = objResult.StatusEffSeq,
                    IsVisible = objResult.IsVisible,
                    Donation = objResult.Donation,
                    StatusDescription = Extension.ToEmpty(objResult.StatusDescription).Trim(),
                    AwardId = Extension.ToEmpty(objResult.AwardId).Trim(),
                    AwardStatus = Extension.ToEmpty(objResult.AwardStatus).Trim(),
                    ImageLink = (objResult.Image == String.Empty || objResult.Image == null) ? "" : Extension.S3Server + objResult.Image,
                    VideoLink = (objResult.Video == String.Empty || objResult.Video == null) ? "" : Extension.S3Server + objResult.Video,
                    Status = objResult.Status
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult EditProject(ProjectEditViewModel model, HttpPostedFileBase imageFile,
              HttpPostedFileBase videoFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            string fileName = string.Empty;
            string Ext = string.Empty;

            string videofileName = string.Empty;
            string ExtVideo = string.Empty;

            byte[] imgData = null;//; new byte[0];
            byte[] videoData = null;//; new byte[0];


            try
            {
                if (imageFile != null)
                {
                    fileName = imageFile.FileName;
                    Ext = Path.GetExtension(imageFile.FileName);
                    imgData = Extension.FileToByteArray(imageFile);

                }

                if (videoFile != null)
                {
                    videofileName = videoFile.FileName;
                    ExtVideo = Path.GetExtension(videoFile.FileName);
                    videoData = Extension.FileToByteArray(videoFile);

                }

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                MProject objEnt = new MProject
                {
                    ProjectId = model.ProjectId,
                    
                    Ext = Ext,
                    ExtVideo = ExtVideo,
                    IsVisible = model.IsVisible,
                    Donation = model.Donation,
                    Image = model.Image,
                    Video = model.Video,
                    FileByte = imgData,
                    VideoFileByte = videoData
                };

                response = new WebApiProject().UpdateProject(objEnt, objSession);
                
                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = (model.ProjectId == 0) ? string.Format(MessageResource.SaveSuccess, "Project") : string.Format(MessageResource.UpdateSuccess, "Project");

                objResult.isError = (statusCode.Equals("2") || statusCode.Equals("1")) ? true : false;
                objResult.message = (statusCode.Equals("2") || statusCode.Equals("1")) ? statusMessage : MessageResul;

            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = string.Format(MessageResource.UpdateError + "Error :" + ex.Message, "Project");
            }
            return Json(objResult);
        }




    }
}
