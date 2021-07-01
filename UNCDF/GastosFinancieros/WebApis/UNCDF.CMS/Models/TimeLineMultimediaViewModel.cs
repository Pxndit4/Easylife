using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class TimeLineMultimediaViewModel
    {
        public int TimeLineMulId { get; set; }
        public int TimeLineId { get; set; }
        [Required]
        public int Type { get; set; }

        [Required]
        public string File { get; set; }
        public string FileLink { get; set; }

        [Required]
        public string Title { get; set; }
        public byte[] FileByte { get; set; }
        public string FileExt { get; set; }
    }
    public class ResultSearchTimeLineMultimediaViewModel
    {
        public int TimeLineMulId { get; set; }
        public int TimeLineId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string File { get; set; }
        public string FileLink { get; set; }
        public string Title { get; set; }


    }

}