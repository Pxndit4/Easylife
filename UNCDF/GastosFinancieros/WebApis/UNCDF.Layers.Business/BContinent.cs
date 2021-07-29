using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BContinent
    {
        public static List<MContinent> Lis(MContinent ent, ref int Val)
        {
            return DAContinent.List(ent, ref Val);
        }
    }
}
