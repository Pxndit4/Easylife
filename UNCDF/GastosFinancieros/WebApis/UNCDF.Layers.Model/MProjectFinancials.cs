using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MProjectFinancials
    {
        public int ProjectId { get; set; }
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
        public string  ImplementAgencyCode { get; set; }
        public decimal Spent { get; set; }
        

    }
}
