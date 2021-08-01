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
        public string Year { get; set; }
        public string GifLoadPresupuesto { get; set; }
        public string GifLoadGasto { get; set; }
        public decimal Budget { get; set; }
        public decimal Expenditure { get; set; }
        public decimal AdvanceBudget { get; set; }
        public decimal AdvanceExpenditure { get; set; }
    }
}
