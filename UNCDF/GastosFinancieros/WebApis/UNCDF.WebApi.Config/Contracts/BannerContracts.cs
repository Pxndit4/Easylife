using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class BannerResponse : BaseResponse
    {
        public MBanner banner { get; set; }
    }

    [Serializable]
    public class BannersResponse : BaseResponse
    {
        public MBanner[] banners { get; set; }
    }

    [Serializable]
    public class BannerRequest : BaseRequest
    {
        public MBanner banner { get; set; }
    }
}
