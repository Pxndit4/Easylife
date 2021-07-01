using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class TimeLineTestimonialTranslateViewModel
    {
        [Display(Name = "Testimonio Id")]
        public int TimeLineTestId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        [Required]
        public string Testimonial { get; set; }
        public string Language { get; set; }
        public ResultSearchTimeLineTestimonialTranslateViewModel Translates { get; set; }
    }
    public class ResultSearchTimeLineTestimonialTranslateViewModel
    {
        public int TimeLineTestId { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        public string Testimonial { get; set; }
        public string Language { get; set; }
    }
}