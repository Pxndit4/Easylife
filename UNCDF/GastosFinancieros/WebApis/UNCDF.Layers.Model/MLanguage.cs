using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MLanguage
    {
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string Flag { get; set; }
        public string FlagOld { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public byte[] FileByte { get; set; }
        public string FlagExtension { get; set; }
    }
}
