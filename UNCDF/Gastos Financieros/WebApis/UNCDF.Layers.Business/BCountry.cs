using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Models;

namespace UNCDF.Layers.Business
{
    public class BCountry
    {
        public static MCountry Select(MCountry ent, ref int Val)
        {
            return DACountry.Select(ent, ref Val);
        }
    }
}
