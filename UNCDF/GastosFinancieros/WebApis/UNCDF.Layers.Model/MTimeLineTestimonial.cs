using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MTimeLineTestimonial
    {
        public int TimeLineTestId { get; set; }
        public int TimeLineId { get; set; }
        public string Name { get; set; }
        public string Testimonial { get; set; }
        public string Photo { get; set; }
        public string Ext { get; set; }
        public byte[] FileByte { get; set; }
    }
}
