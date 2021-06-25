using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class ProjectViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public ResultSearchProjectsViewModel result { get; set; }
    }

    public class ResultSearchProjectsViewModel
    {
        public int ProjectId { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }
        public string EffectiveStatus { get; set; }
        public string StatusEffDateStr { get; set; }
        public int StatusEffDate { get; set; }
        public int StatusEffSeq { get; set; }
        public string StatusDescription { get; set; }
        public string Status { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public string EndDateStr { get; set; }
        public string StartDateStr { get; set; }
        
        public string Title { get; set; }
        public string AwardId { get; set; }
        public string AwardStatus { get; set; }
    }

    public class LoadProjectsViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }

        public ResultLoadProjectsViewModel resulLoad { get; set; }
    }

    public class ResultLoadProjectsViewModel
    {
        public int ProjectId { get; set; }
        [Display(Name = "Project Code")]

        public string Department { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string EffectiveStatus { get; set; }
        public string StatusEffDateStr { get; set; }
        public int StatusEffDate { get; set; }
        

        public int StatusEffSeq { get; set; }
        public string StatusDescription { get; set; }
        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }
        public string Title { get; set; }
        public string AwardId { get; set; }
        public string AwardStatus { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelProjectResult : MProject
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }
}