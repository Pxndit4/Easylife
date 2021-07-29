using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MTimeLine
    {
        public int TimeLineId { get; set; }
        public int ProjectId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string Day { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Date { get; set; }
        public decimal StartDate { get; set; }
        public string DateStr { get; set; }
        
        public decimal EndDate { get; set; }
        public int Advance { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        
        public int Approved { get; set; }
        public string ReasonReject { get; set; }
        public string TitleProject { get; set; }

        public string File { get; set; }
    }
}
