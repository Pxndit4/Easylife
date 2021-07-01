using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MTimeLineMultimedia
    {
        public int TimeLineMulId { get; set; }
        public int TimeLineId { get; set; }
        public int Type { get; set; }
        public string File { get; set; }
        public string Title { get; set; }
        public byte[] FileByte { get; set; }
        public string FileExt { get; set; }
    }
}
