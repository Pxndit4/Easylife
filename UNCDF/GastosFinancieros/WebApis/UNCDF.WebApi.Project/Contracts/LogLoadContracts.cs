using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project
{
    [Serializable]
    public class LogLoadRequest : BaseRequest
    {
        public MLogLoad LogLoad { get; set; }
    }

    [Serializable]
    public class LogLoadListResponse : BaseResponse
    {
        public MLogLoad[] LogLoadList { get; set; }
    }
}
