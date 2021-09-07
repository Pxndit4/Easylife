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

    public class ProjectAddViewModel
    {
        public int ProjectId { get; set; }       
        
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Title { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public string Filter { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Effective Status")]
        public string EffectiveStatus { get; set; }        
        public ResultSearchProjectAddViewModel Result { get; set; }

        //[Display(Name = "")]
        //public List<ResultSearchProjectsViewModel> resultExc { get; set; }
    }

    public class ResultSearchProjectAddViewModel
    {
       
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        
        
        public string ProjectCode { get; set; }
        
        public string Title { get; set; }              
    }



}