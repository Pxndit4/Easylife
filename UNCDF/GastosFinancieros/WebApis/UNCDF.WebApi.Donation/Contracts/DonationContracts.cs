using System;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Donation
{
    [Serializable]
    public class DonationRequest : BaseRequest
    {
        public MDonation Donation { get; set; }
        public MDonorFrequency DonorFrequency { get; set; }
        public PayMethod PayMethod { get; set; }
    }

    [Serializable]
    public class CertificateRequest : BaseRequest
    {
        public int DonationId { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class PayMethod
    {       
        public MDonorStripe DonorStripe { get; set; }
    }

    [Serializable]
    public class PaymentStripeRequest : BaseRequest
    {
        public long Amount { get; set; }
        public int DonorId { get; set; }
    }

    [Serializable]
    public class CancelPaymentStripeRequest : BaseRequest
    {
        public string PaymentId { get; set; }
    }   

    [Serializable]
    public class GetPaymentStripeRequest : BaseRequest
    {
        public string PaymentId { get; set; }
    }

    public class DonationsResponse : BaseResponse
    {
        public MDonation[] Donations { get; set; }
    }

    public class ProjectDonationsResponse : BaseResponse
    {
        public MProjectDonation[] Donations { get; set; }
    }

    [Serializable]
    public class DonationResponse : BaseResponse
    {
        public MDonation Donation { get; set; }
    }

    [Serializable]
    public class DonationTotalResponse : BaseResponse
    {
        public decimal FoodsTotal { get; set; }
        public decimal DonationTotal { get; set; }
    }


}
