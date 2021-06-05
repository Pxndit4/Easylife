using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    #region Response

    [Serializable]
    public class ImplementAgenciesResponse : BaseResponse
    {
        public MImplementAgency[] ImplementAgencies { get; set; }
    }

    #endregion

    [Serializable]
    public class ImplementAgenciesRequest : BaseRequest
    {
        public MImplementAgency[] ImplementAgencies { get; set; }
    }
}
