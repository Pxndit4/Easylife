using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UNCDF.CMS.Models
{
    public class InterfaceControlTranslateViewModel
    {
        public int InterfaceControlId { get; set; }

        [Display(Name = "Language")]
        public string LanguageId { get; set; }

        [Display(Name = "Control Name")]
        public string ControlName { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Description { get; set; }
        public ResultSearchInterfaceControlTransViewModel Translates { get; set; }
    }

    public class ResultSearchInterfaceControlTransViewModel
    {
        public int InterfaceControlId { get; set; }
        public string LanguageId { get; set; }
        [Display(Name = "Content")]
        public string Description { get; set; }

        public string LanguageName { get; set; }
    }
}
