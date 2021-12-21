using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class SearchLogLoadViewModel
    {
        public int LogloadId { get; set; }

        [Display(Name = "Type Load")]
        public string TypeParamId { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        
        
        [Display(Name = "")]
        public ResultSearchLogLoadViewModel Result { get; set; }
    }

    public class ResultSearchLogLoadViewModel
    {
        public int LogloadId { get; set; }
        public int TypeParamId { get; set; }
        
        [Display(Name = "Loading Date")]
        public string LoadingDate { get; set; }
        public int DateId { get; set; }
        public int EndDate { get; set; }
        public int StartDate { get; set; }

        public int UserId { get; set; }
        public int Total { get; set; }
        [Display(Name = "Total Correct")]
        public int TotalCorrectRecords { get; set; }
        [Display(Name = "Total Bad")]
        public int TotalBadRecords { get; set; }

        public string Code { get; set; }
        [Display(Name = "Type")]
        public string DescriptionParam { get; set; }

        [Display(Name = "User")]
        public string NameUser { get; set; }

    }
}