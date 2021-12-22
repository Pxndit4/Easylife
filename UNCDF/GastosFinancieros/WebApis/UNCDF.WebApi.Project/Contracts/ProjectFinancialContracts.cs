using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project
{
    #region Response
    [Serializable]
    public class ProjectFinancialResponse : BaseResponse
    {
        public MProjectFinancials[] ProjectFinancials { get; set; }
    }

    #endregion
    [Serializable]
    public class ProjectFinancialsRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public MProjectFinancials[] ProjectFinancials { get; set; }
    }
}
