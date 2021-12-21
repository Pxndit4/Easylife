using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BLogLoad
    {
        public static int Insert(MLogLoad ent, ref int Val)
        {
            return DALogLoad.Insert(ent,ref Val); ;
        }

        public static List<MLogLoad> List(MLogLoad ent,  ref int Val)
        {
            return DALogLoad.List(ent,ref Val);
        }
    }
}
