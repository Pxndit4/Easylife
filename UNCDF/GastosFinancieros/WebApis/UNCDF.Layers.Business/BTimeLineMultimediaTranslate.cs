using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BTimeLineMultimediaTranslate
    {
        public static List<MTimeLineMultimediaTranslate> Lis(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            return DATimeLineMultimediaTranslate.Lis(ent, ref Val);
        }

        public static int Update(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            return DATimeLineMultimediaTranslate.Update(ent, ref Val);

        }

        public static int Insert(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            return DATimeLineMultimediaTranslate.Insert(ent, ref Val);

        }

        public static MTimeLineMultimediaTranslate Select(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            return DATimeLineMultimediaTranslate.Select(ent, ref Val);
        }

        public static int Delete(MTimeLineMultimediaTranslate ent, ref int Val)
        {
            return DATimeLineMultimediaTranslate.Delete(ent, ref Val);

        }
    }

}
