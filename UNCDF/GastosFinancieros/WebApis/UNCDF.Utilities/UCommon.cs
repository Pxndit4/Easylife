using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace UNCDF.Utilities
{
    public class UCommon
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string GetTokem()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 30).Select(s => s[random.Next(s.Length)]).ToArray());
        }

      
        public static void PDFStamp(string PDFTemp, string PathIMGs)
        {
            string PDFResul = PDFTemp.Replace("[WSTAMP]", "");

            Document Doc = new Document(PageSize.A4);

            using (FileStream fs = new FileStream(PDFResul, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfCopy copy = new PdfCopy(Doc, fs);

                Doc.Open();

                PdfReader Rd = new PdfReader(PDFTemp);

                Int32 n = Rd.NumberOfPages;

                Int32 page = 0;

                while (page < 0)
                {
                    page += 1;
                    copy.AddPage(copy.GetImportedPage(Rd, page));
                }

                copy.FreeReader(Rd);

                PdfStamper stamper = new PdfStamper(Rd, fs);

                Int32 numeroDePaginas = Rd.NumberOfPages;

                BaseFont bf = BaseFont.CreateFont();

                PdfContentByte underContent = null;
                PdfContentByte underContent2 = null;
                PdfContentByte underContent3 = null;
                PdfContentByte underContent4 = null;
                //CREAMOS UN OBJETO IMAGEN PARA PODER ASIGNAR EL LOGO DE LA EMPRESA


                iTextSharp.text.Image img;
                img = iTextSharp.text.Image.GetInstance(Path.Combine(PathIMGs, "background2.png"));
                img.SetAbsolutePosition(10, 100);
                img.ScaleAbsolute(200, 400);


                iTextSharp.text.Image img2;
                img2 = iTextSharp.text.Image.GetInstance(Path.Combine(PathIMGs, "logoUncdfUnitLife.png"));
                img2.SetAbsolutePosition(270, 100);
                img2.ScaleAbsolute(300, 100);
                ////img2.SetAbsolutePosition(40, 790);
                ////img2.ScalePercent(30, 30);

                iTextSharp.text.Image img3;
                img3 = iTextSharp.text.Image.GetInstance(Path.Combine(PathIMGs, "background.png"));
                img3.SetAbsolutePosition(630, 100);
                img3.ScaleAbsolute(200, 400);

                //iTextSharp.text.Image img3;
                //img3 = iTextSharp.text.Image.GetInstance(Path.Combine(PathIMGs, "PlatoIzq.png"));
                //img3.SetAbsolutePosition(16, 20);
                ////img3.SetAbsolutePosition(250, 15);
                //img3.ScalePercent(20, 20);

                //iTextSharp.text.Image img4;
                //img4 = iTextSharp.text.Image.GetInstance(Path.Combine(PathIMGs, "Firma.jpg"));
                //img4.SetAbsolutePosition(600, 90);
                //img4.ScalePercent(30, 30);

                for (int i = 1; i <= numeroDePaginas; i++)
                {
                    underContent = stamper.GetUnderContent(i);
                    underContent.AddImage(img);

                    underContent2 = stamper.GetUnderContent(i);
                    underContent2.AddImage(img2);

                    underContent3 = stamper.GetUnderContent(i);
                    underContent3.AddImage(img3);

                    //underContent4 = stamper.GetUnderContent(i);
                    //underContent4.AddImage(img4);
                }

                stamper.Close();
                Rd.Close();
            }

            File.SetAttributes(PDFTemp, FileAttributes.Normal);
            File.Delete(PDFTemp);
        }

        public static string GetMonthName(int MonthNumber)
        {
            switch (MonthNumber)
            {
                case 1: return "JANUARY";
                case 2: return "FEBRUARY";
                case 3: return "MARCH";
                case 4: return "APRIL";
                case 5: return "MAY";
                case 6: return "JUNE";
                case 7: return "JULY";
                case 8: return "AUGUST";
                case 9: return "SEPTEMBER";
                case 10: return "OCTOBER";
                case 11: return "NOVEMBER";
                case 12: return "DECEMBER";
                default: return "";
            }
        }

        public static string ConvertirFechaDecimalaString(decimal fecha)
        {
            string resultado = "";

            if (fecha == 0 & fecha.ToString().Length == 8)
            {
                string strfecha = fecha.ToString();

                if (strfecha.Length > 0)
                {
                    resultado = strfecha.Substring(6, 2) + "- " + GetMonthName(Convert.ToInt32(strfecha.Substring(4, 2))) + " - " + strfecha.Substring(0, 4);
                }
            }
            else
            {
                return "";
            }

            return resultado;
        }
    }
}
