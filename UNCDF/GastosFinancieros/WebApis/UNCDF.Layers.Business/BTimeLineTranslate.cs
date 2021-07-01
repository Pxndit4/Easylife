using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BTimeLineTranslate
    {
        public static List<MTimeLineTranslate> Lis(MTimeLineTranslate ent, ref int Val)
        {
            return DATimeLineTranslate.Lis(ent, ref Val);
        }

        public static int Update(MTimeLineTranslate ent, ref int Val)
        {
            return DATimeLineTranslate.Update(ent, ref Val);

        }

        public static int Insert(MTimeLineTranslate ent, ref int Val)
        {
            return DATimeLineTranslate.Insert(ent, ref Val);

        }

        public static MTimeLineTranslate Select(MTimeLineTranslate ent, ref int Val)
        {
            return DATimeLineTranslate.Select(ent, ref Val);
        }

        public static int Delete(MTimeLineTranslate ent, ref int Val)
        {
            return DATimeLineTranslate.Delete(ent, ref Val);

        }
    }
}
