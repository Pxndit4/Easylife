using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class IntroductionTranslateResponse : BaseResponse
    {
        public MIntroductionTranslate IntroductionTranslate { get; set; }
    }

    [Serializable]
    public class IntroductionTranslatesResponse : BaseResponse
    {
        public MIntroductionTranslate[] IntroductionTranslates { get; set; }
    }
    [Serializable]
    public class IntroductionTranslateRequest : BaseRequest
    {
        public MIntroductionTranslate MIntroductionTranslate { get; set; }
    }
}
