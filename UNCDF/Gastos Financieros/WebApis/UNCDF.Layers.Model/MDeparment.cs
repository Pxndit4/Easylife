using System;
namespace UNCDF.Layers.Model
{
    public class MDeparment
    {
        public int DeparmentId { get; set; }
        public string DeparmentCode { get; set; }
        public string Description { get; set; }
        public string PracticeArea { get; set; }
        public string Region { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int CountryId { get; set; }
    }
}
