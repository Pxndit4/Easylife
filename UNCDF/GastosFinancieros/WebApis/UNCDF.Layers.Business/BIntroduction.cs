using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BIntroduction
    {
        public static List<MIntroduction> List(MIntroduction ent, BaseRequest baseRequest)
        {
            return DAIntroduction.List(ent, baseRequest);
        }

        public static int Update(MIntroduction ent, ref int Val)
        {
            return DAIntroduction.Update(ent, ref Val);

        }

        public static int Insert(MIntroduction ent, ref int Val)
        {
            return DAIntroduction.Insert(ent, ref Val);

        }

        public static MIntroduction Select(MIntroduction ent, ref int Val)
        {
            return DAIntroduction.Select(ent, ref Val);
        }

        public static int Delete(MIntroduction ent, ref int Val)
        {
            return DAIntroduction.Delete(ent, ref Val);

        }

    }
}
