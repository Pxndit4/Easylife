using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class PracticeAreaViewModel
    {
        public string PracticeArea { get; set; }
        
        public string CheckPracticeAreaCode { get; set; }
        public ResultSearchPracticeAreasViewModel result { get; set; }
    }

    public class ResultSearchPracticeAreasViewModel
    {
        public string PracticeArea { get; set; }
        public int FlagActive { get; set; }

    }
}