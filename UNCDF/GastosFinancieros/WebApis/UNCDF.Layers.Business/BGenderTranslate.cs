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
            return DAGenderTranslate.Lis(ent, ref Val);
        }
        public static int Insert(MGenderTranslate ent, ref int Val)
        {
            return DAGenderTranslate.Insert(ent, ref Val);

        }

        public static int Update(MGenderTranslate ent, ref int Val)
        {
            return DAGenderTranslate.Update(ent, ref Val);

        }

        public static MGenderTranslate Select(MGenderTranslate ent, ref int Val)
        {
            return DAGenderTranslate.Select(ent, ref Val);
        }

        public static int Delete(MGenderTranslate ent, ref int Val)
        {
            return DAGenderTranslate.Delete(ent, ref Val);
        }

    }
}
