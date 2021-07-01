using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project
{
    [Serializable]
    public class TimeLineTranslateResponse : BaseResponse
    {
        public MTimeLineTranslate TimeLineTranslate { get; set; }
    }

    [Serializable]
    public class TimeLineTranslatesResponse : BaseResponse
    {
        public MTimeLineTranslate[] TimeLineTranslates { get; set; }
    }

    [Serializable]
    public class TimeLineTranslateRequest : BaseRequest
    {
        public MTimeLineTranslate TimeLineTranslateBE { get; set; }
    }
}
