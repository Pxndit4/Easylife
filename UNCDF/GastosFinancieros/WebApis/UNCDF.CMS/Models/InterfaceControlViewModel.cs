using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UNCDF.CMS.Models
{
    public class InterfaceControlViewModel
    {
        public int InterfaceControlId { get; set; }
        public int InterfaceId { get; set; }

        [Display(Name = "Interface")]
        public string InterfaceName { get; set; }
        [Display(Name = "Control")]
        public string ControlName { get; set; }

        [Display(Name = "Content")]
        public string Description { get; set; }

        [Display(Name = "Description")]
        public string DescriptionControl { get; set; }

        public string Type { get; set; }

        public ResultSearchInterfaceControlViewModel interfaceControls { get; set; }
    }

    public class SearchInterfaceControlViewModel
    {

        ////[MinLength(3, ErrorMessage = "Código, Minimo 3 caracteres")]
        //public string Description { get; set; }

        //[Display(Name = "Status")]
        ////[MinLength(3, ErrorMessage = "Usuario, Minimo 3 caracteres")]
        //public int Status { get; set; }

        [Display(Name = "")]
        public ResultSearchInterfaceControlViewModel Result { get; set; }
    }

    public class ResultSearchInterfaceControlViewModel
    {
        public int InterfaceControlId { get; set; }
        public int InterfaceId { get; set; }

        [Display(Name = "Control")]
        public string ControlName { get; set; }

        [Display(Name = "Content")]
        public string Description { get; set; }

        [Display(Name = "Description")]
        public string DescriptionControl { get; set; }
    }
}
