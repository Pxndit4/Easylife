using System;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class LanguageResponse : BaseResponse
    {
        public MLanguage Language { get; set; }
    }

    [Serializable]
    public class LanguagesResponse : BaseResponse
    {
        public MLanguage[] Languages { get; set; }
    }

    [Serializable]
    public class LanguageRequest : BaseRequest
    {
        public MLanguage MLanguage { get; set; }
    }
}
