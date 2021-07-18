using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MBanner
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string SubTile { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public string ImageExtension { get; set; }
        public string StatusName { get; set; }
        public byte[] FileByte { get; set; }
    }
}
