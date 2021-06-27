using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.Model;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public class BParameter
    {
        public static List<MParameter> List(MParameter ent, ref int Val)
        {
            return DAParameter.List(ent, ref Val);
        }
    }
}
