using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNCDF.Layers.Model
{
    public class MDonorPayPal
    {
        public int DonorId { get; set; }
        public string Mail { get; set; }
        public string Guid { get; set; }
        public string PaymentId { get; set; }
        public string Token { get; set; }
        public string PayerID { get; set; }
    }
}
