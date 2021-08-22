using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
   public class MGenderTranslate
    {
        public int GenderId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Value { get; set; }
    }
}
