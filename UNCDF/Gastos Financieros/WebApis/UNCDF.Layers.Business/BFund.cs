using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BFund
    {
        public static List<MFund> List()
        {
            return DAFund.List();
        }
    }
}
