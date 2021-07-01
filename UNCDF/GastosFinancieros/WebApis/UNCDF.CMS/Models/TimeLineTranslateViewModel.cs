using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class TimeLineTranslateViewModel
    {
        [Display(Name = "TimeLine Id")]
        public int TimeLineId { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Display(Name = "Title Project")]
        public string TitleProject { get; set; }
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public ResultSearchTimeLineTranslateViewModel Translates { get; set; }
    }

    public class ResultSearchTimeLineTranslateViewModel
    {
        public int TimeLineId { get; set; }
        public string LanguageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
    }

}