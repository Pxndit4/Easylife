using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    public class TimeLinesResponse : BaseResponse
    {
        public MTimeLine[] TimeLines { get; set; }
    }
    public class TimeLineResponse : BaseResponse
    {
        public MTimeLine TimeLine { get; set; }
    }
    [Serializable]
    public class TimeLineRequest : BaseRequest
    {
        public MTimeLine TimeLine { get; set; }
    }

}
