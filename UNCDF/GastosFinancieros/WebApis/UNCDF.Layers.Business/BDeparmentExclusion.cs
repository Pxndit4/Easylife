using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDeparmentExclusion
    {

        public static int Insert(MDeparmentExclusion ent)
        {
           return DADeparmentExclusion.Insert(ent);
        }
        public static int Delete(MDeparmentExclusion ent, ref int val)
        {
            return DADeparmentExclusion.Delete(ent, ref val);
        }
    }
}
