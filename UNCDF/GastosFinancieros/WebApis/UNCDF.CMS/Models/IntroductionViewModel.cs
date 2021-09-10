using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class IntroductionViewModel
    {
        public int IntroductionId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
        public string ImageLink { get; set; }

        public int Status { get; set; }
        public int? Order { get; set; }
        public string ImageExtension { get; set; }
        public byte[] FileByte { get; set; }

    }

    public class SearchIntroductionViewModel
    {
        public string Title { get; set; }
        public int Status { get; set; }

        [Display(Name = "")]
        public ResultSearchIntroductionViewModel Result { get; set; }
    }

    public class ResultSearchIntroductionViewModel
    {
        public int IntroductionId { get; set; }
        public string Title { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        public int Order { get; set; }

        public int Status { get; set; }
    }
}