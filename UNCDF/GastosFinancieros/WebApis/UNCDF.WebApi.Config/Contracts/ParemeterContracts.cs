using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class ParametersResponse : BaseResponse
    {
        public MParameter[] Parameters { get; set; }
    }

    public class ParameterResponse : BaseResponse
    {
        public MParameter Parameter { get; set; }
    }

    [Serializable]
    public class ParameterRequest : BaseRequest
    {
        public MParameter Parameter { get; set; }
    }
}
