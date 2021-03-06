using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Util
{
    public class FileManager
    {
        public static void GenerateExcel(Object listobj, string EntityName)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add(EntityName);
            workSheet.TabColor = System.Drawing.Color.Black;
            string rutaExcel = Path.Combine(HttpRuntime.AppDomainAppPath, "File/Exports/");
            rutaExcel = rutaExcel + EntityName + ".xlsx";

            int recordIndex = 2;
            int rHeadboard = 1;

            //DONOR
            if (listobj is List<MSubscribers>)
            {
                int i = 0;
                //datos
                foreach (var item in (List<MSubscribers>)listobj)
                {
                    i++;

                    if (i == 1)//Headboard
                    {
                        workSheet.Cells[rHeadboard, 1].Value = nameof(item.Id);
                        workSheet.Cells[rHeadboard, 2].Value = nameof(item.Email);
                        workSheet.Cells["A" + rHeadboard.ToString() + ":B" + rHeadboard.ToString()].Style.Font.Bold = true;
                    }
                    
                    workSheet.Cells[recordIndex, 1].Value = item.Id;
                    workSheet.Cells[recordIndex, 2].Value = item.Email;
                    recordIndex++;
                }

            }
            //DONOR
            else if (listobj is List<MDonor>)
            {
                int i = 0;
                //datos
                foreach (var item in (List<MDonor>)listobj)
                {
                    i++;

                    if (i == 1)//Headboard
                    {
                        workSheet.Cells[rHeadboard, 1].Value = nameof(item.Name);
                        workSheet.Cells[rHeadboard, 2].Value = nameof(item.Email);
                        workSheet.Cells[rHeadboard, 3].Value = "Cell phone";//nameof(item.Cellphone);
                        workSheet.Cells[rHeadboard, 4].Value = nameof(item.Continent);
                        workSheet.Cells[rHeadboard, 5].Value = nameof(item.Country);
                        workSheet.Cells[rHeadboard, 6].Value = "Birthday";//item.BirthdayStr;
                        workSheet.Cells[rHeadboard, 7].Value = "Registered";// item.RegisteredName;
                        workSheet.Cells[rHeadboard, 8].Value = "Donated";// item.RegisteredName;
                        
                        workSheet.Cells["A" + rHeadboard.ToString() + ":H" + rHeadboard.ToString()].Style.Font.Bold = true;
                    }
                    //if (recordIndex == 2)//body
                    //{

                    workSheet.Cells[recordIndex, 1].Value = item.Name;
                    workSheet.Cells[recordIndex, 2].Value = item.Email;
                    workSheet.Cells[recordIndex, 3].Value = item.Cellphone;
                    workSheet.Cells[recordIndex, 4].Value = item.Continent;
                    workSheet.Cells[recordIndex, 5].Value = item.Country;
                    workSheet.Cells[recordIndex, 6].Value = item.BirthdayStr;
                    workSheet.Cells[recordIndex, 7].Value = item.RegisteredName;
                    workSheet.Cells[recordIndex, 8].Value = item.Donated;
                    recordIndex++;
                    //}
                }

            }

            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();


            String excelname = EntityName;
            FileInfo fi = new FileInfo(rutaExcel);

            excel.SaveAs(fi);

        }
    }
}