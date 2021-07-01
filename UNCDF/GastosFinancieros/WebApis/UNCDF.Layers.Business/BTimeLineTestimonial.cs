using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
   public class BTimeLineTestimonial
    {
        public static List<MTimeLineTestimonial> List(MTimeLineTestimonial ent, BaseRequest baseRequest, ref int Val)
        {
            return DATimeLineTestimonial.List(ent, baseRequest, ref Val);
        }

        public static List<MTimeLineTestimonial> RandomLis(BaseRequest baseRequest, ref int Val)
        {
            return DATimeLineTestimonial.RandomLis(baseRequest, ref Val);
        }


        public static List<MTimeLineTestimonial> Filter(MTimeLineTestimonial ent, ref int Val)
        {
            return DATimeLineTestimonial.Filter(ent, ref Val);
        }


        public static int Insert(MTimeLineTestimonial ent, ref int Val)
        {
            return DATimeLineTestimonial.Insert(ent, ref Val);

        }

        public static int Update(MTimeLineTestimonial ent, ref int Val)
        {
            return DATimeLineTestimonial.Update(ent, ref Val);

        }

        public static MTimeLineTestimonial Select(MTimeLineTestimonial ent, ref int Val)
        {
            return DATimeLineTestimonial.Select(ent, ref Val);
        }

        public static int Delete(MTimeLineTestimonial ent, ref int Val)
        {
            return DATimeLineTestimonial.Delete(ent, ref Val);
        }

    }
}
