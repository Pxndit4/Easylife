using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
   public class BTimeLineTestimonialTranslate
    {

        public static List<MTimeLineTestimonialTranslate> Lis(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            return DATimeLineTestimonialTranslate.Lis(ent, ref Val);
        }

        public static int Update(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            return DATimeLineTestimonialTranslate.Update(ent, ref Val);

        }

        public static int Insert(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            return DATimeLineTestimonialTranslate.Insert(ent, ref Val);

        }

        public static MTimeLineTestimonialTranslate Select(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            return DATimeLineTestimonialTranslate.Select(ent, ref Val);
        }

        public static int Delete(MTimeLineTestimonialTranslate ent, ref int Val)
        {
            return DATimeLineTestimonialTranslate.Delete(ent, ref Val);

        }
    }
}
