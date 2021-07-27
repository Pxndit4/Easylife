using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNCDF.CMS.Models
{
    public class ProjectExplusionsViewModel
    {
        public ResultSearchProjectExcViewModel resultProjExcl { get; set; }
        public ResultSearchPracticeAreaExcViewModel resultPracticeExcl { get; set; }
        public ResultSearchDeparmentExcViewModel resultDepartExc { get; set; }
    }
    public class ResultSearchProjectExcViewModel
    {
        public string  ProjectCode { get; set; }
    }
    public class ResultSearchPracticeAreaExcViewModel
    {
        public string PracticeArea { get; set; }
    }

    public class ResultSearchDeparmentExcViewModel
    {
        public string DeparmentCode { get; set; }
    }
}