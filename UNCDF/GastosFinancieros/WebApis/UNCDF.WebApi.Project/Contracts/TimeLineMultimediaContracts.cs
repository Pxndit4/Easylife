using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project
{
    public class TimeLineMultimediasResponse : BaseResponse
    {
        public MTimeLineMultimedia[] TimeLineMultimedias { get; set; }
    }
    public class TimeLineMultimediaResponse : BaseResponse
    {
        public MTimeLineMultimedia TimeLineMultimedia { get; set; }
    }
    [Serializable]
    public class TimeLineMultimediaRequest : BaseRequest
    {
        public MTimeLineMultimedia TimeLineMultimedia { get; set; }
    }
}
