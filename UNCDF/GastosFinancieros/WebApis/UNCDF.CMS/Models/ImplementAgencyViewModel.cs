using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class ImplementAgencyViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        [Display(Name = "ImplementAgency Code")]
        public string ImplementAgencyCode { get; set; }
        public string Description { get; set; }
        public ResultSearchImplementAgencysViewModel result { get; set; }
    }

    public class ResultSearchImplementAgencysViewModel
    {
        public int ImplementAgencyId { get; set; }
        [Display(Name = "ImplementAgency Code")]
        public string ImplementAgencyCode { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }

    public class LoadImplementAgencysViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }

        public int Total { get; set; }
        [Display(Name = "Correct Records")]
        public int TotalCorrectRecords { get; set; }
        [Display(Name = "Bad Records")]
        public int TotalBadRecords { get; set; }

        public ResultLoadImplementAgencysViewModel resulLoad { get; set; }
    }

    public class ResultLoadImplementAgencysViewModel
    {
        public int ImplementAgencyId { get; set; }
        [Display(Name = "ImplementAgency Code")]
        public string ImplementAgencyCode { get; set; }
        public string Description { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelImplementAgencyResult : MImplementAgency
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }

        public int Total { get; set; }
        public int TotalCorrectRecords { get; set; }
        public int TotalBadRecords { get; set; }
    }
}