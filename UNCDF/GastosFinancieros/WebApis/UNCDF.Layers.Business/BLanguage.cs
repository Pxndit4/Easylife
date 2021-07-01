using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BLanguage
    {
        public static List<MLanguage> Lis(ref int Val)
        {
            return DALanguage.Lis(ref Val);
        }

        public static int Insert(MLanguage ent, ref int Val)
        {
            return DALanguage.Insert(ent, ref Val);

        }

        public static int Update(MLanguage ent, ref int Val)
        {
            return DALanguage.Update(ent, ref Val);

        }

        public static MLanguage Select(MLanguage ent, ref int Val)
        {
            return DALanguage.Select(ent, ref Val);
        }

        public static List<MLanguage> Filter(MLanguage ent, ref int Val)
        {
            return DALanguage.Filter(ent, ref Val);
        }

        public static int Delete(MLanguage ent, ref int Val)
        {
            return DALanguage.Delete(ent, ref Val);
        }
    }
}
