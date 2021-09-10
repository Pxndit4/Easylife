using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MIntroduction
    {
        public int IntroductionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? Order { get; set; }
        public string ImageExtension { get; set; }
        public byte[] FileByte { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
    }
}
