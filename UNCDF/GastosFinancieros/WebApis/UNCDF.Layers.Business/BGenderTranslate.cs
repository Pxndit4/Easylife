using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BGenderTranslate
    {
        public static List<MGenderTranslate> List(MGenderTranslate ent, ref int Val)
        {
            return BGenderTranslate.List(ent, ref Val);
        }
        public static int Insert(MGenderTranslate ent, ref int Val)
        {
            return BGenderTranslate.Insert(ent, ref Val);

        }

        public static int Update(MGenderTranslate ent, ref int Val)
        {
            return BGenderTranslate.Update(ent, ref Val);

        }

        public static MGenderTranslate Select(MGenderTranslate ent, ref int Val)
        {
            return BGenderTranslate.Select(ent, ref Val);
        }

        public static int Delete(MGenderTranslate ent, ref int Val)
        {
            return BGenderTranslate.Delete(ent, ref Val);
        }

    }
}
