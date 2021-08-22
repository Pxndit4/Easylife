using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class GenderTranslateViewModel
    {

        [Display(Name = "Gender Id")]
        public int GenderId { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Description")]
        public string DescriptionName { get; set; }
        //public string Language { get; set; }
        [Required]
        public string Value { get; set; }
        public ResultSearchGenderTransViewModel Translates { get; set; }
    }

    public class ResultSearchGenderTransViewModel
    {
        public int GenderId { get; set; }
        public string LanguageId { get; set; }
        public string Description { get; set; }

        public string Value { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }
    }

}