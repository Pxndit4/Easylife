using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class BannerTranslateResponse : BaseResponse
    {
        public MBannerTranslate BannerTranslate { get; set; }
    }

    [Serializable]
    public class BannerTranslatesResponse : BaseResponse
    {
        public MBannerTranslate[] BannerTranslates { get; set; }
    }
    [Serializable]
    public class BannerTranslateRequest : BaseRequest
    {
        public MBannerTranslate MBannerTranslate { get; set; }
    }
}
