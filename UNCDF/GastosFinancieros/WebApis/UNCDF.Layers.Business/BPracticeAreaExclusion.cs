using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BPracticeAreaExclusion
    {
        public static int Insert(MPracticeAreaExclusion ent)
        {
            return DAPracticeAreaExclusion.Insert(ent);
        }
        public static int Delete(MPracticeAreaExclusion ent, ref int val)
        {
            return DAPracticeAreaExclusion.Delete(ent, ref val);
        }
        public static List<MPracticeAreaExclusion> List(MPracticeAreaExclusion ent, ref int Val)
        {
            return DAPracticeAreaExclusion.List(ent, ref Val);
        }
    }
}
