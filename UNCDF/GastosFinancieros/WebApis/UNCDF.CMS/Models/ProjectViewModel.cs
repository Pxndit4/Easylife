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
        public int ProjectId { get; set; }
        public byte[] FileByte { get; set; }
        public string File { get; set; }
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
        public string CheckProjectCode { get; set; }
        public ResultSearchProjectsViewModel result { get; set; }

        [Display(Name = "")]
        public List<ResultSearchProjectsViewModel> resultExc { get; set; }
    }



    public class ResultSearchProjectsViewModel
    {
        public int ProjectId { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }
        [Display(Name = "Effective Status")]
        public string EffectiveStatus { get; set; }

        [Display(Name = "Status Eff Date")] 
        public string StatusEffDateStr { get; set; }
        public int StatusEffDate { get; set; }

        [Display(Name = "Status Eff Seq")] 
        public int StatusEffSeq { get; set; }

        
        [Display(Name = "Status Description")]
        public string StatusDescription { get; set; }

        
        [Display(Name = "Project Status")]
        public string Status { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        
        [Display(Name = "End Date")]
        public string EndDateStr { get; set; }

        [Display(Name = "Start Date")]
        public string StartDateStr { get; set; }
        
        public string Title { get; set; }
        [Display(Name = "Award Id")]
        public string AwardId { get; set; }

        [Display(Name = "Award Status")]
        public string AwardStatus { get; set; }
        public int FlagActive { get; set; }

        
    }

    public class LoadProjectsViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }
        public int Total { get; set; }
        [Display(Name = "Correct Records")]
        public int TotalCorrectRecords { get; set; }
        [Display(Name = "Bad Records")]
        public int TotalBadRecords { get; set; }

        public ResultLoadProjectsViewModel resulLoad { get; set; }
    }

    public class ResultLoadProjectsViewModel
    {
        public int ProjectId { get; set; }
        

        public string Department { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        [Display(Name = "Effective Status")]
        public string EffectiveStatus { get; set; }

        [Display(Name = "Status Eff Date")]
        public string StatusEffDateStr { get; set; }
        public int StatusEffDate { get; set; }

        [Display(Name = "Status Eff Seq")]
        public int StatusEffSeq { get; set; }
        [Display(Name = "Status Description")]
        public string StatusDescription { get; set; }
        [Display(Name = "Start Date")]
        public string StartDateStr { get; set; }
        [Display(Name = "End Date")]
        public string EndDateStr { get; set; }
        public string Title { get; set; }
        [Display(Name = "Award Id")]
        public string AwardId { get; set; }
        [Display(Name = "Award Status")]
        public string AwardStatus { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelProjectResult : MProject
    {
        public int Total { get; set; }
        public int TotalCorrectRecords { get; set; }
        public int TotalBadRecords { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ProjectEditViewModel 
    {
        public int ProjectId { get; set; }
        public string Department { get; set; }

        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        [Display(Name = "Project Status")]
        public string Status { get; set; }

        [Display(Name = "Effective Status")]
        public string EffectiveStatus { get; set; }

        [Display(Name = "Status Eff Date")]
        public string StatusEffDateStr { get; set; }
        public int StatusEffDate { get; set; }

        [Display(Name = "Status Eff Seq")] 
        public int StatusEffSeq { get; set; }

        [Display(Name = "Status Description")]
        public string StatusDescription { get; set; }

        [Display(Name = "Start Date")]
        public string StartDateStr { get; set; }

        [Display(Name = "End Date")]
        public string EndDateStr { get; set; }
        public string Title { get; set; }
        [Display(Name = "Award Id")]
        public string AwardId { get; set; }

        [Display(Name = "Award Status")]
        public string AwardStatus { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string ImageLink { get; set; }
        public string VideoLink { get; set; }
        [Display(Name = "Visible")]
        public bool IsVisible { get; set; }
        public bool Donation { get; set; }
        public bool IsVisibleBool { get; set; }
    }

    public class ProjectExclusionViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Title { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Effective Status")]
        public string EffectiveStatus { get; set; }

        public ResultDeparmentExclViewModel result { get; set; }
    }

    public class ResultDeparmentExclViewModel
    {
        [Display(Name = "Deparment Code")]
        public string DeparmentCode { get; set; }
    }

    public class ResultProjectExclViewModel
    {
        [Display(Name = "Projec Code")]
        public string ProjecCode { get; set; }
    }

    public class ResultPracticeAreaExclViewModel
    {
        [Display(Name = "Practice Area")]
        public string PracticeArea { get; set; }
    }
}