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

    public class DeparmentResponse : BaseResponse
    {
        public MDeparment Deparment { get; set; }
    }

    #endregion

    [Serializable]
    public class DeparmentsRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public MDeparment[] Deparments { get; set; }
    }

    [Serializable]
    public class DeparmentRequest : BaseRequest
    {
        public MDeparment Deparment { get; set; }
    }
}
