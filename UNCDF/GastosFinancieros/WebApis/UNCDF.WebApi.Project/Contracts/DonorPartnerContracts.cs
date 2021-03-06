using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    #region Response

    [Serializable]
    public class DonorPartnersResponse : BaseResponse
    {
        public MDonorPartner[] DonorPartners { get; set; }
    }

    #endregion

    [Serializable]
    public class DonorPartnersRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public MDonorPartner[] DonorPartners { get; set; }
    }
}
