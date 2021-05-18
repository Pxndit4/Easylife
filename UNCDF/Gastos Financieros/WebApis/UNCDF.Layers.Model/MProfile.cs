using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Model
{
    public class MProfile
    {
        public int ProfileId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public List<MProfileOptions> Options { get; set; }
    }
}
