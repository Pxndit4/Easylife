using HiQPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDonation
    {
        public static List<MDonation> List(MDonation ent, ref int Val)
        {
            return DADonation.Lis(ent, ref Val);
        }

        public static BaseResponse Insert(MDonation ent, MDonorFrequency donorFrequencyBE, MPayMethod payMethodBE, BaseRequest baseRequest)
        {
            BaseResponse baseResponse = new BaseResponse();

            int CodeResult = DADonation.Insert(ent);

            baseResponse.Code = CodeResult.ToString();

            if (CodeResult == 0)
            {
                if (donorFrequencyBE != null)
                {
                    int CodeResult2 = BDonorFrequency.Insert(donorFrequencyBE, baseRequest);

                    baseResponse.Code = CodeResult2.ToString();

                    if (CodeResult2 == 0)
                    {
                        int CodeResult3 = 0;

                        if (ent.PaymentType.Equals("4")) //STRIPE
                        {
                            MDonorStripe donorCriptoBE = new MDonorStripe();
                            donorCriptoBE.DonationId = ent.DonationId;
                            donorCriptoBE.PaymentId = payMethodBE.DonorStripe.PaymentId;

                            CodeResult3 = BDonorStripe.Confirm(donorCriptoBE, baseRequest);
                        }

                        baseResponse.Code = CodeResult3.ToString();

                        if (CodeResult3 == 0)
                        {
                            baseResponse.Message = "Success";
                        }
                        else
                        {
                            baseResponse.Message = "An error occurred while registering the payment method";
                        }

                        return baseResponse;
                    }
                    else
                    {
                        baseResponse.Message = "An error occurred while recording the payment frequency";
                    }

                    return baseResponse;
                }
                else
                {
                    if (ent.PaymentType.Equals("4")) //STRIPE
                    {
                        MDonorStripe donorCriptoBE = new MDonorStripe();
                        donorCriptoBE.DonationId = ent.DonationId;
                        donorCriptoBE.PaymentId = payMethodBE.DonorStripe.PaymentId;

                        CodeResult = BDonorStripe.Confirm(donorCriptoBE, baseRequest);
                    }

                    if (CodeResult == 0)
                    {
                        baseResponse.Message = "Success";
                    }
                    else
                    {
                        baseResponse.Message = "Error confirming payment stripe";
                    }

                }
            }
            else
            {
                baseResponse.Message = "An error occurred while registering the donation";
            }

            return baseResponse;
        }

        public static int Update(MDonation ent, BaseRequest baseRequest)
        {
            return DADonation.Update(ent, baseRequest);
        }

        public static List<decimal> GetTotals()
        {
            return DADonation.GetTotals();
        }

        public static MDonation Select(MDonation ent, ref int Val)
        {
            return DADonation.Select(ent, ref Val);
        }

        public static string GenarteCerticate(int DonorId, string webRoot, string Amount, MAwsS3 mAwsS3)
        {
            string CertificateName = string.Empty;
            string CertificatePath = string.Empty;

            Uri webRootUri = new Uri(webRoot);
            string pathAbs = webRootUri.AbsolutePath;

            CertificatePath = Path.Combine(pathAbs, "Certificate");

            int val = 0;
            string DonorName = string.Empty;


            MDonor donor = new MDonor();
            donor.DonorId = DonorId;
            donor = BDonor.Select(donor, ref val);

            if (val.Equals(0))
            {
                DonorName = donor.FirstName + " " + donor.LastName;

                if (DonorName.Trim().Equals("")) DonorName = "Donor";
            }
            else
            {
                DonorName = "Donor";
            }

            string Day = DateTime.Now.ToString("dd");
            string Year = DateTime.Now.Year.ToString();
            string Month = Utilities.UCommon.GetMonthName(DateTime.Now.Month);
            CertificateName = "Certificate_" + DateTime.Now.ToString("ddMMyyyy_hh_mm_ss") + "[WSTAMP].pdf";
            CertificatePath = Path.Combine(CertificatePath, CertificateName);

            System.IO.StreamReader sr = new StreamReader(@"Certificate\certificado.html");
            string Certificate = sr.ReadToEnd().ToString();

            Certificate = Certificate.Replace("[DONOR_NAME]", DonorName).Replace("[MONTH]", Month).Replace("[DAY]", Day).Replace("[ANIO]", Year).Replace("[AMOUNT]", Amount);

            //TextWriter tw = NUtilitarios.GenerarContenidoPDFDesdeNVelocity(strPlantilla, datosClaveValor);
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Landscape;
            htmlToPdfConverter.Document.Margins = new PdfMargins(5);

            htmlToPdfConverter.ConvertHtmlToFile(Certificate, "", CertificatePath);

            Utilities.UCommon.PDFStamp(CertificatePath, Path.Combine(pathAbs, "Certificate"));

            string PDFResul = CertificatePath.Replace("[WSTAMP]", "");

            if (!BAwsSDK.UploadS3(mAwsS3, PDFResul, "certificates", CertificateName.Replace("[WSTAMP]", "")))
            {
                CertificateName = "Ocurrio un error al cargar el Certificado de Donación";
            }

            System.IO.File.Delete(PDFResul);

            return CertificateName;
        }
    }
}
