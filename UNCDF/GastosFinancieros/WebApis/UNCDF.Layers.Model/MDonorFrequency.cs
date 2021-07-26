using System;
namespace UNCDF.Layers.Model
{
    public class MDonorFrequency
    {
        public int DonorId { get; set; }
        public int Quantity { get; set; }
        public int Frequency { get; set; }
        public decimal Amount { get; set; }
        public decimal Date { get; set; }
        public int PaymentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
