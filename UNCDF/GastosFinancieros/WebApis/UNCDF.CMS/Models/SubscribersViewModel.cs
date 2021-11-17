using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS.Models
{
    
    public class SubscribersViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class SearchSubscribersViewModel
    {
        public string Email { get; set; }

        [Display(Name = "")]
        public ResultSearchSubscribersViewModel Result { get; set; }
    }
    public class ResultSearchSubscribersViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

}