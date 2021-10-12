using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class SubscribersResponse : BaseResponse
    {
        public MSubscribers Subscribers { get; set; }
    }

    [Serializable]
    public class SubscribersRequest : BaseRequest
    {
        public MSubscribers Subscribers { get; set; }
    }
}
