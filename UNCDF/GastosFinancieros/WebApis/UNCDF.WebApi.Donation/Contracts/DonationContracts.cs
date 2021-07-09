using System;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Donation
{
    [Serializable]
    public class DonationRequest : BaseRequest
    {
        public MDonation Donation { get; set; }
    }

    public class DonationsResponse : BaseResponse
    {
        public MDonation[] Donations { get; set; }
    }

    [Serializable]
    public class DonationResponse : BaseResponse
    {
        public MDonation Donation { get; set; }
    }
}
