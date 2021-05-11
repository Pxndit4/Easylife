using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    public class Session
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string DateLogin { get; set; }
    }
}
