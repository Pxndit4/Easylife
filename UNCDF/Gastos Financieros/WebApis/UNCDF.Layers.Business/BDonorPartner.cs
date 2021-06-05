using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDonorPartner
    {
        public static List<MDonorPartner> List()
        {
            return DADonorPartner.List();
        }

        public static int Insert(MDonorPartner ent)
        {
            return DADonorPartner.Insert(ent);
        }
    }
}
