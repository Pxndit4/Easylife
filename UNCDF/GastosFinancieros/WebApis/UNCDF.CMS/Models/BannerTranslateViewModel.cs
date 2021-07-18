using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class BannerTranslateViewModel
    {
        [Display(Name = "Banner Id")]
        public int BannerId { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Title")]
        public string TitleName { get; set; }
        //public string Language { get; set; }
        [Required]
        public string SubTitle { get; set; }
        public ResultSearchBannerTransViewModel Translates { get; set; }
    }

    public class ResultSearchBannerTransViewModel
    {
        public int BannerId { get; set; }
        public string LanguageId { get; set; }
        public string Title { get; set; }

        public string SubTitle { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }
    }

}