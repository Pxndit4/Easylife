using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class IntroductionResponse : BaseResponse
    {
        public MIntroduction[] Introductions { get; set; }
    }

    public class IntroductionsResponse : BaseResponse
    {
        public MIntroduction[] Introductions { get; set; }
    }

    public class IntroductionBEResponse : BaseResponse
    {
        public MIntroduction Introduction { get; set; }
    }

    [Serializable]
    public class IntroductionRequest : BaseRequest
    {
        public MIntroduction Introduction { get; set; }
    }
}
