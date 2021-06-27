using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Model
{
    [Serializable]
    public class BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
