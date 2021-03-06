using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{

    #region Response

    [Serializable]
    public class FundsResponse : BaseResponse
    {
        public MFund[] Funds { get; set; }
    }

    #endregion

    [Serializable]
    public class FundsRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set;}
        public MFund[] Funds { get; set; }
    }

}
