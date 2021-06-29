using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BInterfaceControlTranslate
    {
        public static List<MInterfaceControlTranslate> List(MInterfaceControlTranslate ent, ref int Val)
        {
            return DAInterfaceControlTranslate.Lis(ent, ref Val);
        }
        public static int Insert(MInterfaceControlTranslate ent, ref int Val)
        {
            return DAInterfaceControlTranslate.Insert(ent, ref Val);

        }

        public static int Update(MInterfaceControlTranslate ent, ref int Val)
        {
            return DAInterfaceControlTranslate.Update(ent, ref Val);

        }

        public static MInterfaceControlTranslate Select(MInterfaceControlTranslate ent, ref int Val)
        {
            return DAInterfaceControlTranslate.Select(ent, ref Val);
        }

        public static int Delete(MInterfaceControlTranslate ent, ref int Val)
        {
            return DAInterfaceControlTranslate.Delete(ent, ref Val);
        }
    }
}
