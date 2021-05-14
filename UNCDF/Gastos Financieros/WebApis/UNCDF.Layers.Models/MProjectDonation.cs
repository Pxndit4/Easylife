using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    public class MProjectDonation
    {
        public int DonationId { get; set; }
        public string Date { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public decimal SumAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] ListDonations { get; set; }

        public String Image { get; set; }
    }
}
