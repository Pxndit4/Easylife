using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MDonation
    {
        public int DonationId { get; set; }
        public int DonorId { get; set; }
        public decimal Date { get; set; }
        public decimal Amount { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public string PaymentType { get; set; }
        public string Certificate { get; set; }

        public int OngId { get; set; }
        public string Ong { get; set; }
        public string Project { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal StartDate { get; set; }
        public decimal EndDate { get; set; }
        public string  DateStr { get; set; }
        public string Email { get; set; }
        //Adecuación Proyecto deseado
        public int ProjectId { get; set; }
    }

    public class MPayMethod
    {
        public MDonorStripe DonorStripe { get; set; }
        public MDonorPayPal DonorPayPal { get; set; }
    }
}
