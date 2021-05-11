using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    public class MParameter
    {
        public int ParameterId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public int Status { get; set; }
    }
}
