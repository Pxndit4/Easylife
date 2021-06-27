using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Model
{
    public class MUser
    {
        public int UserId { get; set; }
        public int Type { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }
    }
}
