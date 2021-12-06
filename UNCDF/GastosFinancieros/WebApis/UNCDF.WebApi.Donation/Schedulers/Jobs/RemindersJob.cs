using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Donation.Schedulers
{
    public class RemindersJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly MAwsEmail _MAwsEmail;
        private readonly IWebHostEnvironment _env;

        public RemindersJob(IServiceProvider provider, IOptions<MAwsEmail> MAwsEmail, IWebHostEnvironment env)
        {
            _provider = provider;
            _MAwsEmail = MAwsEmail.Value;
            _env = env;
        }
        public Task Execute(IJobExecutionContext context)
        {
            MDonorFrequency ent = new MDonorFrequency();
            List<MDonorFrequency> Donations = new List<MDonorFrequency>();
            var dateString = DateTime.Now.ToString("yyyyMMdd");

            ent.Date = Convert.ToDecimal(dateString);

            int val = 0;

            Donations = BDonorFrequency.Select(ent, ref val);

            foreach (MDonorFrequency item in Donations)
            {
                string Message = "Dear donor. We remind you that you have a donation scheduled for " + Utilities.UCommon.ConvertirFechaDecimalaString(ent.Date);
                string Subject = "UNCDF - Next donation " + Utilities.UCommon.ConvertirFechaDecimalaString(ent.Date);

                string webRoot = _env.ContentRootPath;

                _MAwsEmail.Subject = Subject;
                _MAwsEmail.Message = GetBodyMail(webRoot, Message);
                _MAwsEmail.ToEmail = item.Email;

                BAwsSDK.SendEmailAsync(_MAwsEmail);

                BDonorFrequency.update(item);
            }

            return Task.CompletedTask;
        }

        public static string GetBodyMail(string webRoot, string Messasge)
        {
            string TemplateMail = string.Empty;
            Uri webRootUri = new Uri(webRoot);
            string pathAbs = webRootUri.AbsolutePath;
            int val = 0;

            TemplateMail = Path.Combine(pathAbs, "Certificate");

            MParameter MParameter = new MParameter();
            List<MParameter> MParameters = new List<MParameter>();

            MParameter.Code = "SOCIAL";
            MParameters = BParameter.List(MParameter, ref val);

            string Display = "style = 'display:none;'";

            System.IO.StreamReader sr = new StreamReader(@"Certificate\TemplateMail.html");
            TemplateMail = sr.ReadToEnd().ToString();
            TemplateMail = TemplateMail.Replace("[Message]", Messasge);

            int ExistFB = 0, ExistIN = 0, ExistsTw = 0, ExistYT = 0;

            foreach (MParameter item in MParameters)
            {
                if (item.Description.Equals("Facebook")) if (!item.Valor1.Equals("")) ExistFB = 1;
                if (item.Description.Equals("Instagram")) if (!item.Valor1.Equals("")) ExistIN = 1;
                if (item.Description.Equals("Twitter")) if (!item.Valor1.Equals("")) ExistsTw = 1;
                if (item.Description.Equals("Youtube")) if (!item.Valor1.Equals("")) ExistYT = 1;
            }

            if (ExistFB.Equals(0)) TemplateMail = TemplateMail.Replace("[Display_FB]", Display); else TemplateMail.Replace("[Display_FB]", "");
            if (ExistIN.Equals(0)) TemplateMail = TemplateMail.Replace("[Display_INS]", Display); else TemplateMail.Replace("[Display_INS]", "");
            if (ExistsTw.Equals(0)) TemplateMail = TemplateMail.Replace("[Display_TWI]", Display); else TemplateMail.Replace("[Display_TWI]", "");
            if (ExistYT.Equals(0)) TemplateMail = TemplateMail.Replace("[Display_YOU]", Display); else TemplateMail.Replace("[Display_YOU]", "");

            return TemplateMail;
        }

        public void Logs(string message)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Quartz");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "Logs.txt");
            using FileStream fstream = new FileStream(path, FileMode.Create);
            using TextWriter writer = new StreamWriter(fstream);
            writer.WriteLine(message);
        }
    }
}
