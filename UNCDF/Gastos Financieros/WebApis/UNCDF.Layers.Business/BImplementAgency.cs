﻿using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BImplementAgency
    {
        public static List<MImplementAgency> List()
        {
            return DAImplementAgency.List();
        }
    }
}
