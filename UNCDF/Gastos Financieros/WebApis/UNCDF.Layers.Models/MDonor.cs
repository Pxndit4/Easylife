using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    public class MDonor
    {
        public int DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Gender { get; set; }
        public decimal Birthday { get; set; }
        public string Photo { get; set; }
        public int Status { get; set; }
        public int Registered { get; set; }
        public string Token { get; set; }
        public string OldPassword { get; set; }
    }
}
