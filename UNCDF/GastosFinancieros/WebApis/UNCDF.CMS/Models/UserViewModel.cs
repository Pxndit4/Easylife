using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UNCDF.CMS.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "User")]
        [MaxLength(100, ErrorMessage = "User, Maximum 100 characters")]
        [RegularExpression(@"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)", ErrorMessage = "Mail data is not formatted properly")]
        public string User { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [MinLength(2, ErrorMessage = "User Name, Minimum 2 characters")]
        [MaxLength(100, ErrorMessage = "User Name, Maximum 100 characters")]
        public string Name { get; set; }
        public int Type { get; set; }
        public string Status { get; set; }
    }

    public class SearchUserViewModel
    {
        [Display(Name = "User")]
        //[MinLength(3, ErrorMessage = "Código, Minimo 3 caracteres")]
        public string User { get; set; }

        [Display(Name = "User Name")]
        //[MinLength(3, ErrorMessage = "Usuario, Minimo 3 caracteres")]
        public string UserName { get; set; }

        [Display(Name = "")]
        public ResultSearchUserViewModel Result { get; set; }
    }

    public class ResultSearchUserViewModel
    {
        [Display(Name = "Id")]
        public int UserId { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }

        [Display(Name = "User Name")]
        public string Name { get; set; }
        public string Status { get; set; }
    }
}