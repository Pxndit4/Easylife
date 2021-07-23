using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDonorFrequency
    {
        public static int Insert(MDonorFrequency ent, BaseRequest baseRequest)
        {
            return DADonorFrequency.Insert(ent, baseRequest);
        }

        public static List<MDonorFrequency> Select(MDonorFrequency ent, ref int Val)
        {
            return DADonorFrequency.Select(ent, ref Val);
        }

        public static int update(MDonorFrequency ent)
        {
            return DADonorFrequency.Update(ent);
        }
    }
}
