using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.Business
{
    public class BBanner
    {
        public static List<MBanner> RandomLis(BaseRequest baseRequest, ref int Val)
        {
            return DABanner.RandomLis(baseRequest, ref Val);
        }
        public static int Insert(MBanner ent, ref int Val)
        {
            return DABanner.Insert(ent, ref Val);

        }

        public static int Update(MBanner ent, ref int Val)
        {
            return DABanner.Update(ent, ref Val);

        }

        public static MBanner Select(MBanner ent, ref int Val)
        {
            return DABanner.Select(ent, ref Val);
        }

        public static List<MBanner> List(MBanner ent, ref int Val)
        {
            return DABanner.List(ent, ref Val);
        }

        public static int Delete(MBanner ent, ref int Val)
        {
            return DABanner.Delete(ent, ref Val);
        }
    }
}
