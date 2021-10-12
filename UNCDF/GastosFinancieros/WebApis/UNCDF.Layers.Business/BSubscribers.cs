using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BSubscribers
    {
        public static int Insert(MSubscribers ent, ref int Val)
        {
            return DASubscribers.Insert(ent, ref Val);
        }
    }
}
