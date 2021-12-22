using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS.Models
{
    public class ProjectFinancialViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Only Numbers")]
        public string Year { get; set; }

        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }

        public ResultSearchProjectFinancialViewModel result { get; set; }
    }

    public class ResultSearchProjectFinancialViewModel
    {

        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Year { get; set; }
        public string GifLoadPresupuesto { get; set; }
        public string GifLoadGasto { get; set; }
        public decimal Budget { get; set; }
        public decimal Balance { get; set; }

        public decimal Expenditure { get; set; }
        public decimal AdvanceBudget { get; set; }
        public decimal AdvanceExpenditure { get; set; }
        [Display(Name = "Oper Unit")]
        public string OperUnit { get; set; }
        [Display(Name = "Project Manager")]
        public string ProjectManager { get; set; }
        [Display(Name = "Deparment Code")]
        public string DeparmentCode { get; set; }

        [Display(Name = "Short Desc")]
        public string ShortDesc { get; set; }

        [Display(Name = "Fund Code")]
        public string FundCode { get; set; }

        [Display(Name = "Descr Fund")]
        public string DescrFund { get; set; }
        [Display(Name = "Pre Encumbrance")]
        public decimal PreEncumbrance { get; set; }
        
        public decimal Encumbrance { get; set; }
        public decimal Disbursement { get; set; }
        [Display(Name = "Project")]
        public string DescrProject { get; set; }
        [Display(Name = "Imp. Agency Code")]
        public string ImplementAgencyCode { get; set; }
        public decimal Spent { get; set; }
    }

    public class LoadProjectFinancialsViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }

        public int Total { get; set; }
        [Display(Name = "Correct Records")]
        public int TotalCorrectRecords { get; set; }
        [Display(Name = "Bad Records")]
        public int TotalBadRecords { get; set; }

        public ResultLoadProjectFinaViewModel resulLoad { get; set; }
    }

    public class ResultLoadProjectFinaViewModel
    {
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }
        public string Year { get; set; }
        public string GifLoadPresupuesto { get; set; }
        public string GifLoadGasto { get; set; }
        public decimal Budget { get; set; }
        public decimal Balance { get; set; }

        public decimal Expenditure { get; set; }
        public decimal AdvanceBudget { get; set; }
        public decimal AdvanceExpenditure { get; set; }
        [Display(Name = "Oper Unit")]
        public string OperUnit { get; set; }
        [Display(Name = "Project Manager")]
        public string ProjectManager { get; set; }
        [Display(Name = "Deparment Code")]
        public string DeparmentCode { get; set; }

        [Display(Name = "Short Desc")]
        public string ShortDesc { get; set; }

        [Display(Name = "Fund Code")]
        public string FundCode { get; set; }

        [Display(Name = "Descr Fund")]
        public string DescrFund { get; set; }
        [Display(Name = "Pre Encumbrance")]
        public decimal PreEncumbrance { get; set; }

        public decimal Encumbrance { get; set; }
        public decimal Disbursement { get; set; }
        [Display(Name = "Project")]
        public string DescrProject { get; set; }
        [Display(Name = "Imp. Agency Code")]
        public string ImplementAgencyCode { get; set; }
        public decimal Spent { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelProjectFinancialResult : MProjectFinancials
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }

        public int Total { get; set; }
        public int TotalCorrectRecords { get; set; }
        public int TotalBadRecords { get; set; }
    }
}