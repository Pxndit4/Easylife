using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project
{


    [Serializable]
    public class TimeLineTestimonialTranslateRequest : BaseRequest
    {
        public MTimeLineTestimonialTranslate TimeLineTestimonialTranslate { get; set; }
    }


    [Serializable]
    public class TimeLineTestimonialTranslateResponse : BaseResponse
    {
        public MTimeLineTestimonialTranslate TimeLineTestimonialTranslate { get; set; }
    }

    [Serializable]
    public class TimeLineTestimonialTranslatesResponse : BaseResponse
    {
        public MTimeLineTestimonialTranslate[] TimeLineTestimonialTranslates { get; set; }
    }
}
