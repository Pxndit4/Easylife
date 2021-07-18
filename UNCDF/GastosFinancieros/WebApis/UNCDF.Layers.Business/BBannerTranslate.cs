using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BBannerTranslate
    {
        public static List<MBannerTranslate> Lis(MBannerTranslate ent, ref int Val)
        {
            return DABannerTranslate.Lis(ent, ref Val);
        }

        public static int Update(MBannerTranslate ent, ref int Val)
        {
            return DABannerTranslate.Update(ent, ref Val);

        }

        public static int Insert(MBannerTranslate ent, ref int Val)
        {
            return DABannerTranslate.Insert(ent, ref Val);

        }

        public static MBannerTranslate Select(MBannerTranslate ent, ref int Val)
        {
            return DABannerTranslate.Select(ent, ref Val);
        }

        public static int Delete(MBannerTranslate ent, ref int Val)
        {
            return DABannerTranslate.Delete(ent, ref Val);

        }
    }
}
