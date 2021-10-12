using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MProjectExclusion
    {
        public string ProjectCode { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string PracticeArea { get; set; }
        public int IsActive { get; set; }
        
        public string[] ListCode { get; set; }
    }
}
