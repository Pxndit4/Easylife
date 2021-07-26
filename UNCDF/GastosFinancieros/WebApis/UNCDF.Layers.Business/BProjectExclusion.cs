using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BProjectExclusion
    {
        public static int Insert(MProjectExclusion ent)
        {
            return DAProjectExclusion.Insert(ent);
        }
        public static int Delete(MProjectExclusion ent, ref int val)
        {
            return DAProjectExclusion.Delete(ent, ref val);
        }
    }
}
