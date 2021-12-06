using System;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Donation
{
    [Serializable]
    public class DonorRequest : BaseRequest
    {
        public MDonor Donor { get; set; }
    }

    [Serializable]
    public class DonorResponse : BaseResponse
    {
        public MDonor[] Donors { get; set; }
    }
}
