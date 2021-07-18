using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MBannerTranslate
    {
        public int BannerId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Language { get; set; }
    }
}
