using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MUserProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string User { get; set; }
        public string Profile { get; set; }
        public string Name { get; set; }
        public string ProjectCode { get; set; }
        public int EndDate { get; set; }
        public int StartDate { get; set; }
        public string EffectiveStatus { get; set; }
        public string Title { get; set; }
    }
}
