using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BInterfaceControl
    {
        public static int Insert(MInterfaceControl ent, ref int Val)
        {
            return DAInterfaceControl.Insert(ent, ref Val);

        }

        public static int Update(MInterfaceControl ent, ref int Val)
        {
            return DAInterfaceControl.Update(ent, ref Val);

        }

        public static MInterfaceControl Select(MInterfaceControl ent, ref int Val)
        {
            return DAInterfaceControl.Select(ent, ref Val);
        }

        public static List<MInterfaceControl> List(MInterfaceControl ent, ref int Val)
        {
            return DAInterfaceControl.List(ent, ref Val);
        }

        public static int Delete(MInterfaceControl ent, ref int Val)
        {
            return DAInterfaceControl.Delete(ent, ref Val);
        }
    }
}
