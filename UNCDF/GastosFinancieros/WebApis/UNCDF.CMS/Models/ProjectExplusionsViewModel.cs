using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UNCDF.CMS.Models
{
    public class ProjectExplusionsViewModel
    {
        public ResultSearchProjectExcViewModel resultProjExcl { get; set; }
        public ResultSearchPracticeAreaExcViewModel resultPracticeExcl { get; set; }
        public ResultSearchDeparmentExcViewModel resultDepartExc { get; set; }
        //public ResultSearchProjectsViewModel resultProjects { get; set; }
    }
    public class ResultSearchProjectExcViewModel
    {
        public int ProjectId { get; set; }
        public int IsActive { get; set; }

        [Display(Name = "Project Code")]
        public string  ProjectCode { get; set; }
        public string Title { get; set; }
        [Display(Name = "Practice Area")]
        public string PracticeArea { get; set; }
    }
    public class ResultSearchPracticeAreaExcViewModel
    {
        [Display(Name = "Practice Area")]
        public string PracticeArea { get; set; }
    }

    public class ResultSearchDeparmentExcViewModel
    {
        public string DeparmentCode { get; set; }
    }
}