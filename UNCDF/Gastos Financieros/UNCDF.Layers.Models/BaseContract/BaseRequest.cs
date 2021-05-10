﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Models
{
    [Serializable]
    public class BaseRequest
    {
        public Session Session { get; set; }
        public string Language { get; set; }

        public string ApplicationToken { get; set; }
    }
}
