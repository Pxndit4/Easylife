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
        public string Year { get; set; }
        public ResultSearchProjectFinancialViewModel result { get; set; }
    }

    public class ResultSearchProjectFinancialViewModel
    {
        public string ProjectCode { get; set; }
        public string Year { get; set; }
        public string GifLoadPresupuesto { get; set; }
        public string GifLoadGasto { get; set; }
        public decimal Budget { get; set; }
        public decimal Balance { get; set; }

        public decimal Expenditure { get; set; }
        public decimal AdvanceBudget { get; set; }
        public decimal AdvanceExpenditure { get; set; }
        public string OperUnit { get; set; }
        public string ProjectManager { get; set; }
        public string DeparmentCode { get; set; }
        public string ShortDesc { get; set; }
        public string FundCode { get; set; }
        public string DescrFund { get; set; }
        public decimal PreEncumbrance { get; set; }
        public decimal Encumbrance { get; set; }
        public decimal Disbursement { get; set; }
        public string DescrProject { get; set; }
        public string ImplementAgencyCode { get; set; }
        public decimal Spent { get; set; }
    }

    public class LoadProjectFinancialsViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }

        public ResultLoadProjectFinaViewModel resulLoad { get; set; }
    }

    public class ResultLoadProjectFinaViewModel
    {
        public string ProjectCode { get; set; }
        public string Year { get; set; }
        public string GifLoadPresupuesto { get; set; }
        public string GifLoadGasto { get; set; }
        public decimal Budget { get; set; }
        public decimal Balance { get; set; }

        public decimal Expenditure { get; set; }
        public decimal AdvanceBudget { get; set; }
        public decimal AdvanceExpenditure { get; set; }
        public string OperUnit { get; set; }
        public string ProjectManager { get; set; }
        public string DeparmentCode { get; set; }
        public string ShortDesc { get; set; }
        public string FundCode { get; set; }
        public string DescrFund { get; set; }
        public decimal PreEncumbrance { get; set; }
        public decimal Encumbrance { get; set; }
        public decimal Disbursement { get; set; }
        public string DescrProject { get; set; }
        public string ImplementAgencyCode { get; set; }
        public decimal Spent { get; set; }
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelProjectFinancialResult : MProjectFinancials
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }
}