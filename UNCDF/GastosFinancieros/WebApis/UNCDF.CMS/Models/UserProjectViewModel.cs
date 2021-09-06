using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNCDF.CMS.Models
{
    public class SearchUserProjectViewModel
    {
        
        public string User { get; set; }
        public string Name { get; set; }

        [Display(Name = "")]
        public SearchUserProjectResultadoViewModel Result { get; set; }
    }

    public class SearchUserProjectResultadoViewModel
    {
        [Display(Name = "Id")]
        public int UserId { get; set; }

        public string User { get; set; }

        public string Name { get; set; }
        
        public string Profile { get; set; }

    }


    public class AddUserProjectViewModel
    {
        public int UserId { get; set; }

        public string User { get; set; }

        [Display(Name = "User")]
        public string Name { get; set; }

        [Display(Name = "")]
        public List<ResultUserProjectViewModel> Result { get; set; }

        public ResultUserProjectViewModel line { get; set; }
    }

    public class ResultUserProjectViewModel
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }

        [Display(Name = "Project")]
        public string Title { get; set; }
        
    }

}