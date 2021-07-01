using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    public class TimeLineTestimonialsResponse : BaseResponse
    {
        public MTimeLineTestimonial[] Testimonials { get; set; }
    }

    public class TimeLineTestimonialResponse : BaseResponse
    {
        public MTimeLineTestimonial Testimonial { get; set; }
    }


    [Serializable]
    public class TimeLineTestimonialRequest : BaseRequest
    {
        public MTimeLineTestimonial Testimonial { get; set; }
    }
}
