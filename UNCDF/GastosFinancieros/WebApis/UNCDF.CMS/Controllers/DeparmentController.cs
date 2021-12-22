using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNCDF.Layers.Model;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using UNCDF.CMS.Models;

namespace UNCDF.CMS.Controllers
{
    public class DeparmentController : Controller
    {
        // GET: Deparment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(DeparmentViewModel model)
        {
            JSonResult objResult = new JSonResult();
            try
            {

                List<MDeparment> entList = new List<MDeparment>();

                entList = new WebApiDeparment().GetDeparments();

                model.DeparmentCode = model.DeparmentCode ?? "";
                model.Description = model.Description ?? "";

                if (!model.DeparmentCode.Equals(""))
                {
                    entList = entList.Where(p => p.DeparmentCode.Contains(model.DeparmentCode)).ToList();
                }

                if (!model.Description.Equals(""))
                {
                    entList = entList.Where(p => p.Description.Contains(model.Description)).ToList();
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
            ViewBag.Title = "Deparments - Load File";
            ViewBag.Confirm = string.Format(MessageResource.SaveConfirm, "Deparments");
            Session["ListDonors"] = null;


            return View("Load", new LoadDeparmentsViewModel());
        }

        [HttpPost]
        public ActionResult LoadFile(LoadDeparmentsViewModel model, HttpPostedFileBase imageFile)
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

            var include = new[] { "A", "B", "C", "D" };
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
                    //objResult.data = null;
                    objResult.data = new List<ModelDeparmentResult>();
                    objResult.message = string.Format("Error: Please check the template for this upload ", "Deparments");
                    return Json(objResult);
                }

                if (dt.Rows.Count <= 0)
                {
                    objResult.isError = true;
                    objResult.data = new List<ModelDeparmentResult>();
                    objResult.message = string.Format("The uploaded file has no rows", "Deparments");
                    return Json(objResult);
                }

                try
                {
                    var dtResultado = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
                    dt = dtResultado.CopyToDataTable();

                    List<ModelDeparmentResult> entlist = new List<ModelDeparmentResult>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModelDeparmentResult ent = new ModelDeparmentResult();
                        ent.DeparmentCode = Extension.ToEmpty(dt.Rows[i][0].ToString()).Substring(1, dt.Rows[i][0].ToString().Length - 1);//Convert.ToInt32(dt.Rows[i]["StudentId"]);
                        ent.Description = Extension.ToEmpty(dt.Rows[i][1].ToString());
                        ent.PracticeArea = Extension.ToEmpty(dt.Rows[i][2].ToString());
                        ent.Region = Extension.ToEmpty(dt.Rows[i][3].ToString());
                        ent.AlertMessage = string.Empty;
                        ent.WithAlert = "N";

                        if (ent.DeparmentCode.Length > 10)
                        {
                            ent.AlertMessage += "<tr><td> - the Deparment Code column must not must not exceed 10 characters </td></tr> ";
                        }

                        if (ent.DeparmentCode.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Deparment Code column is required </td></tr> ";
                        }

                        if (ent.Description.Length > 255)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column must not must not exceed 255 characters </td></tr> ";
                        }

                        if (ent.Description.Length == 0)
                        {
                            ent.AlertMessage += "<tr><td> - the Description column is required </td></tr> ";
                        }

                        if (ent.PracticeArea.Length > 100)
                        {
                            ent.AlertMessage += "<tr><td> - the Practice Area column must not must not exceed 100 characters </td></tr> ";
                        }

                        if (ent.Region.Length > 100)
                        {
                            ent.AlertMessage += "<tr><td> - the Region column must not must not exceed 100 characters </td></tr> ";
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

                    Session["ListDonors"] = entlist;
                    objResult.data = entlist;
                }
                catch (Exception ex)
                {
                    objResult.isError = true;
                    objResult.data = new List<ModelDeparmentResult>();
                    objResult.message = "Deparments :" + "Format error, check records";
                    return Json(objResult);
                }

                objResult.isError = false;
                objResult.message = null; // string.Format(MessageResource.SaveSuccess, "Load File save"); 
            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = new List<ModelDeparmentResult>();
                objResult.message = "Error loading Deparments";
            }

            return Json(objResult);
        }

        [HttpPost]
        public JsonResult SearchLoad()
        {
            JSonResult objResult = new JSonResult();
            try
            {
                List<ModelDeparmentResult> entList = new List<ModelDeparmentResult>();
                entList = (List<ModelDeparmentResult>)Session["ListDonors"];
                // Session["ListProjectFinancials"] = null;
                objResult.data = entList;

            }
            catch (Exception ex)
            {
                objResult.data = null;
                objResult.isError = true;
                objResult.message = string.Format(MessageResource.ControllerGetExceptionMessage, "Deparments");
            }

            return Json(objResult);
        }

        public ActionResult Edit(string id)
        {
            MDeparment objResult;
            ViewBag.Title = "Edit Deparment";
            ViewBag.Confirm = string.Format(MessageResource.UpdateConfirm, "Deparment");            

            try
            {

                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                MDeparment eProjects = new MDeparment
                {
                    DeparmentId = Convert.ToInt32(id)
                };

                ViewBag.Countries = new WebApiCountry().GetCountries(new MCountry
                {
                    Continents = ""
                }, objSession).Select(x => new SelectListItem
                {
                    Value = (x.CountryId).ToString(),
                    Text = x.Description
                });

                objResult = new WebApiDeparment().GetDeparment(eProjects);

                return View("Edit", new DeparmentViewModel()
                {
                    DeparmentId = objResult.DeparmentId,
                    DeparmentCode = objResult.DeparmentCode,
                    Description = objResult.Description,
                    PracticeArea = objResult.PracticeArea,
                    Region = objResult.Region,
                    Latitude = objResult.Latitude,
                    Longitude = objResult.Longitude,
                    CountryId = objResult.CountryId                    
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("viewError", MessageResource.PartialViewLoadError);
                return View("_ErrorView");
            }
        }

        [HttpPost]
        public ActionResult EditDeparment(DeparmentViewModel model)
        {
            JSonResult objResult = new JSonResult();

            try
            {                
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };


                MDeparment objEnt = new MDeparment
                {
                    DeparmentId = model.DeparmentId,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    CountryId = model.CountryId
                };

                string response = string.Empty;
                response = new WebApiDeparment().UpdateDeparment(objEnt, objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                string MessageResul = (model.DeparmentId == 0) ? string.Format(MessageResource.SaveSuccess, "Deparment") : string.Format(MessageResource.UpdateSuccess, "Deparment");

                objResult.isError = (statusCode.Equals("2") || statusCode.Equals("1")) ? true : false;
                objResult.message = (statusCode.Equals("2") || statusCode.Equals("1")) ? statusMessage : MessageResul;

            }
            catch (Exception ex)
            {
                objResult.isError = true;
                objResult.data = null;
                objResult.message = string.Format(MessageResource.UpdateError + "Error :" + ex.Message, "Deparment");
            }
            return Json(objResult);
        }

        [HttpPost]
        public ActionResult Register(LoadDeparmentsViewModel model, HttpPostedFileBase imageFile)
        {
            JSonResult objResult = new JSonResult();
            string response = string.Empty;

            try
            {
                Session objSession = new Session()
                {
                    UserId = AutenticationManager.GetUser().IdUsuario,
                };

                List<MDeparment> entList = new List<MDeparment>();
                List<ModelDeparmentResult> entListData = new List<ModelDeparmentResult>();
                entListData = (List<ModelDeparmentResult>)Session["ListDonors"];

                var TotalCorrectRecords = 0;
                var TotalBadRecords = 0;

                foreach (ModelDeparmentResult item in entListData)
                {
                    MDeparment mDonorPartner = new MDeparment();
                    mDonorPartner.DeparmentCode = item.DeparmentCode;
                    mDonorPartner.Description = item.Description;
                    mDonorPartner.PracticeArea = item.PracticeArea;
                    mDonorPartner.Region = item.Region;
                   
                    entList.Add(mDonorPartner);

                    TotalCorrectRecords = item.TotalCorrectRecords;
                    TotalBadRecords = item.TotalBadRecords;
                }

                response = new WebApiDeparment().InsertDeparment(entList, TotalCorrectRecords, TotalBadRecords,  objSession);

                string statusCode = response.Split('|')[0];
                string statusMessage = response.Split('|')[1];

                objResult.isError = statusCode.Equals("2");
                objResult.message = statusCode.Equals("2") ? statusMessage : string.Format(MessageResource.SaveSuccess, "Deparments"); ;
            }
            catch (Exception ex)
            {
                objResult.message = string.Format(MessageResource.SaveError + "Error :" + ex.Message, "Deparments");
            }
            return Json(objResult);
        }
    }
}