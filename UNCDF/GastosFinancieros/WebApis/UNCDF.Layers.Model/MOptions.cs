using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Model
{
    public class MOptions
    {
        public int OptionId { get; set; }
        public int IdFather { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }
        public int Action { get; set; }
        public int Orders { get; set; }
        public string Description { get; set; }
        public int ProfileId { get; set; }
        public int FlagActive { get; set; }
        public string TitleSubModule { get; set; }
        public string TitleModule { get; set; }
    }
}
