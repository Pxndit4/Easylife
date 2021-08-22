using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class GenderTranslateRequest : BaseRequest
    {
        public MGenderTranslate GenderTranslateBE { get; set; }
    }

    [Serializable]
    public class GenderTranslateResponse : BaseResponse
    {
        public MGenderTranslate GenderTranslate { get; set; }
    }

    [Serializable]
    public class GenderTranslatesResponse : BaseResponse
    {
        public MGenderTranslate[] GenderTranslates { get; set; }
    }


}
