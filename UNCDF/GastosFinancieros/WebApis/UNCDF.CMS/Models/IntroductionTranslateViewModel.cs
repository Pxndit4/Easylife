using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class IntroductionTranslateViewModel
    {

        [Display(Name = "Introduction Id")]
        public int IntroductionId { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Title")]
        public string TitleName { get; set; }
        //public string Language { get; set; }
        [Required]
        public string Title { get; set; }
        public ResultSearchIntroductionTransViewModel Translates { get; set; }
    }

    public class ResultSearchIntroductionTransViewModel
    {
        public int IntroductionId { get; set; }
        public string LanguageId { get; set; }
        public string Description { get; set; }

        public string Title { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }
    }

}