using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    #region Response

    [Serializable]
    public class DeparmentsResponse : BaseResponse
    {
        public MDeparment[] Deparments { get; set; }
    }

    #endregion

    [Serializable]
    public class DeparmentsRequest : BaseRequest
    {
        public MDeparment[] Deparments { get; set; }
    }
}
