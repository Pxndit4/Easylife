using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    public class MCountry
    {
        public int CountryId { get; set; }
        public int ContinentId { get; set; }
        public string ContinentName { get; set; }
        public string Description { get; set; }
        public string Flag { get; set; }
        public int Status { get; set; }
        public string Prefix { get; set; }
        public string FlagOld { get; set; }
        public byte[] FileByte { get; set; }
        public string FlagExtension { get; set; }
    }
}
