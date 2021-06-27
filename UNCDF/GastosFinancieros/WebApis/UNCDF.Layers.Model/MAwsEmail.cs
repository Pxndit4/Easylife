using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Model
{
    public class MAwsEmail
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string VerifiedFromEmail { get; set; }
        public string SMTPClient { get; set; }
        public string Port { get; set; }
        public string ToEmail { get; set; }
    }
}
