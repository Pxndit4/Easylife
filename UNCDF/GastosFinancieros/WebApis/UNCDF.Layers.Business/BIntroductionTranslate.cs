using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BIntroductionTranslate
    {
        public static List<MIntroductionTranslate> Lis(MIntroductionTranslate ent, ref int Val)
        {
            return DAIntroductionTranslate.Lis(ent, ref Val);
        }

        public static int Update(MIntroductionTranslate ent, ref int Val)
        {
            return DAIntroductionTranslate.Update(ent, ref Val);

        }

        public static int Insert(MIntroductionTranslate ent, ref int Val)
        {
            return DAIntroductionTranslate.Insert(ent, ref Val);

        }

        public static MIntroductionTranslate Select(MIntroductionTranslate ent, ref int Val)
        {
            return DAIntroductionTranslate.Select(ent, ref Val);
        }

        public static int Delete(MIntroductionTranslate ent, ref int Val)
        {
            return DAIntroductionTranslate.Delete(ent, ref Val);

        }
    }
}
