using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class PanelViewModel
    {
        public string PerfilId { get; set; }
        public List<PanelTab> Items { get; set; }

    }

    public class PanelTab
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<PanelTabItem> Items { get; set; }

    }
    public class PanelTabItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool IsSeparator { get; set; }

    }
}