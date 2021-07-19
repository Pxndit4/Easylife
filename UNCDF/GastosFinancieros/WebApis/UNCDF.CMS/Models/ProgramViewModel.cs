using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class ProgramViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
        public string DonorCode { get; set; }
        public ResultSearchProgramViewModel result { get; set; }
    }

    public class ResultSearchProgramViewModel
    {
        public int ProgramNameId { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
        public string DonorCode { get; set; }        
    }

    public class LoadProgamViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }

        public ResultLoadProgamViewModel resulLoad { get; set; }
    }

    public class ResultLoadProgamViewModel
    {
        public int ProgramNameId { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
        [Display(Name = "Donor Code")]
        public string DonorCode { get; set; }
        [Display(Name = "Details")]
        public string ProjectDetails { get; set; }
        public string Sector { get; set; }
        
        public string SDG { get; set; }
        [Display(Name = "Task Manager")]
        public string TaskManager { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelProgramResult : MProgramName
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }
}