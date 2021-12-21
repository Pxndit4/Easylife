using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class FundViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        [Display(Name = "Fund Code")]
        public string FundCode { get; set; }
        public string Description { get; set; }
        public ResultSearchFundsViewModel result { get; set; }
    }

    public class ResultSearchFundsViewModel
    {
        public int FundId { get; set; }
        [Display(Name = "Fund Code")]
        public string FundCode { get; set; }
        public string Description { get; set; }
    }

    public class LoadFundsViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }
        public int Total { get; set; }
        [Display(Name = "Correct Records")]
        public int TotalCorrectRecords { get; set; }
        [Display(Name = "Bad Records")]
        public int TotalBadRecords { get; set; }
        public ResultLoadFundsViewModel resulLoad { get; set; }
    }

    public class ResultLoadFundsViewModel
    {
        public int FundId { get; set; }
        [Display(Name = "Fund Code")]
        public string FundCode { get; set; }
        public string Description { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelFundResult : MFund
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
        public int Total { get; set; }
        public int TotalCorrectRecords { get; set; }
        public int TotalBadRecords { get; set; }
    }
}