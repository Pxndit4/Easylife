using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class GenderViewModel
    {
        public int GenderId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Value { get; set; }
        public int Status { get; set; }

    }

    public class SearchGenderViewModel
    {
        public string Description { get; set; }
        public int Status { get; set; }

        [Display(Name = "")]
        public ResultSearchGenderViewModel Result { get; set; }
    }

    public class ResultSearchGenderViewModel
    {
        public int GenderId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }
        public int Status { get; set; }
    }
}