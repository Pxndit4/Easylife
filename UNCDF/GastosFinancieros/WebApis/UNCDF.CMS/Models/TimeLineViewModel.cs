using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;


namespace UNCDF.CMS.Models
{
    public class TimeLineViewModel
    {
        public int TimeLineId { get; set; }
        [Display(Name = "Project Id")]
        public int ProjectId { get; set; }

        [Display(Name = "Title Project")]
        public string TitleProject { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string Date { get; set; }

        public string StatusName { get; set; }

        [Required]
        [Display(Name = "New progress")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Only Numbers")]
        [Range(0, 100, ErrorMessage = "Advance must be between 0 and 100")]
        public int Advance { get; set; }

        [Display(Name = "Current progress")]
        public int CurrentAdvance { get; set; }

        [Required]
        [Display(Name = "Reason for Reject")]
        public string ReasonReject { get; set; }

        public int Status { get; set; }
        public ResultSearchTimeLineViewModel eTimeLines { get; set; }

        public ResultSearchTimeLineMultimediaViewModel multimediaList { get; set; }

        public ResultSearchTimeLineTestimonialViewModel testimonials { get; set; }

        //public ResultSearchTimeLineViewModel eTimeLines { get; set; }

        //public ResultSearchTimeLineViewModel eTimeLines { get; set; }
    }

    public class SearchTimeLineViewModel
    {
        public int Approved { get; set; }

        [Display(Name = "")]
        public ResultSearchTimeLineViewModel Result { get; set; }
    }

    public class ResultSearchTimeLineViewModel
    {
        public int TimeLineId { get; set; }
        public int ProjectId { get; set; }

        [Display(Name = "Time Line")]
        public string Title { get; set; }
        [Display(Name = "Project")]
        public string TitleProject { get; set; }
        public string Date { get; set; }

        [Display(Name = "Date")]
        public string DateStr { get; set; }

        [Display(Name = "Reason")]
        public string ReasonReject { get; set; }
        [Display(Name = "Status")]
        public string StatusName { get; set; }
        public int Status { get; set; }

    }


}