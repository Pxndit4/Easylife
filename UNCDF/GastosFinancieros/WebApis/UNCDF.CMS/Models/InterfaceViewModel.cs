using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace UNCDF.CMS.Models
{
    public class InterfaceViewModel
    {
        public int InterfaceId { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        [Display(Name = "Name")]
        public string InterfaceName { get; set; }
        public string ControlName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }


    }

    public class SearchInterfaceViewModel
    {
        [Display(Name = "Name")]
        //[MinLength(3, ErrorMessage = "Código, Minimo 3 caracteres")]
        public string InterfaceName { get; set; }

        //[Display(Name = "Description")]
        //public string Description { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }


        public string Type { get; set; }

        [Display(Name = "Status")]
        //[MinLength(3, ErrorMessage = "Usuario, Minimo 3 caracteres")]
        public int Status { get; set; }

        [Display(Name = "")]
        public ResultSearchInterfaceViewModel Result { get; set; }
    }

    public class ResultSearchInterfaceViewModel
    {
        public int InterfaceId { get; set; }

        [Display(Name = "Tipo")]
        public int TypeId { get; set; }

        [Display(Name = "Name")]
        public string InterfaceName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        [Display(Name = "Status")]
        public string StatusName { get; set; }
    }
}
