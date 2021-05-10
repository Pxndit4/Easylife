using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    public class MAwsS3
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
    }
}
