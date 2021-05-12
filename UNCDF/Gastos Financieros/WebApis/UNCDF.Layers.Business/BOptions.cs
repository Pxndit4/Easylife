using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.Models;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public class BOptions
    {
        public static List<MOptions> Lis(MOptions ent, ref int Val)
        {
            return DAOptions.Lis(ent, ref Val);
        }

        public static List<MOptions> Sel(MOptions ent, ref int Val)
        {
            return DAOptions.Sel(ent, ref Val);
        }
    }
}
