using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class LanguageViewModel
    {
        public int LanguageId { get; set; }

        [Required]
        [Display(Name = "Language")]
        [MaxLength(400, ErrorMessage = "Description, Maximum 400 characters")]
        public string Description { get; set; }

        [Required]
        public string Flag { get; set; }
        public string FlagLink { get; set; }


        public string FlagOld { get; set; }

        [Required]
        [Display(Name = "Code")]
        [MaxLength(10, ErrorMessage = "Code, Maximum 10 characters")]
        public string Code { get; set; }

        public int Status { get; set; }

    }

    public class SearchLanguageViewModel
    {
        [Display(Name = "Description")]
        //[MinLength(3, ErrorMessage = "Código, Minimo 3 caracteres")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        //[MinLength(3, ErrorMessage = "Usuario, Minimo 3 caracteres")]
        public int Status { get; set; }

        [Display(Name = "")]
        public ResultSearchLanguageViewModel Result { get; set; }
    }

    public class ResultSearchLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Flag")]
        public string Flag { get; set; }



        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Status")]
        public int StatusName { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }
    }


}