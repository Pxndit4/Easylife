using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class ParameterViewModel
    {
        public int ParameterId { get; set; }
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Value 1")]
        public string Valor1 { get; set; }
        [Display(Name = "Value 2")]
        public string Valor2 { get; set; }
        public int Status { get; set; }
    }

    public class SearchParameterViewModel
    {
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "")]
        public ResultSearchParameterViewModel Result { get; set; }
    }

    public class ResultSearchParameterViewModel
    {
        public int ParameterId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Value 1")]
        public string Valor1 { get; set; }
        [Display(Name = "Value 2")]
        public string Valor2 { get; set; }
        public int Status { get; set; }
    }
}