using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class DeparmentViewModel
    {
        public int DeparmentId { get; set; }
        public byte[] FileByte { get; set; }
        public string File { get; set; }

        [Display(Name = "Deparment Code")]
        public string DeparmentCode { get; set; }
        public string Description { get; set; }
        public string PracticeArea { get; set; }
        public string Region { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int CountryId { get; set; }
        public string CheckDeparmentCode { get; set; }
        public ResultSearchDeparmentsViewModel result { get; set; }

    }



    public class ResultSearchDeparmentsViewModel
    {
        public int DeparmentId { get; set; }

        [Display(Name = "Deparment Code")]
        public string DeparmentCode { get; set; }
        public string Description { get; set; }
        public string PracticeArea { get; set; }
        public string Region { get; set; }
        public int FlagActive { get; set; }

    }
}