using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class TimeLineTestimonialViewModel
    {
        public int TimeLineTestId { get; set; }
        public int TimeLineId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Testimonial { get; set; }
        [Required]
        public string Photo { get; set; }
        public string PhotoLink { get; set; }

        public string Ext { get; set; }
        public byte[] FileByte { get; set; }
    }

    public class ResultSearchTimeLineTestimonialViewModel
    {
        public int TimeLineTestId { get; set; }
        public int TimeLineId { get; set; }
        public string Name { get; set; }
        public string Testimonial { get; set; }
        public string Photo { get; set; }
        public string PhotoLink { get; set; }

    }


}