using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MLogLoad
    {
        public int LogloadId { get; set; }
        public int TypeParamId { get; set; }
        //public DateTime LoadingDate { get; set; }
        public string LoadingDate { get; set; }
        public int DateId { get; set; }
        public int EndDate { get; set; }
        public int StartDate { get; set; }
        
        public int UserId { get; set; }
        public int Total { get; set; }
        public int TotalCorrectRecords { get; set; }
        public int TotalBadRecords { get; set; }

        public string Code { get; set; }
        public string DescriptionParam { get; set; }
        public string NameUser { get; set; }





    }
}
