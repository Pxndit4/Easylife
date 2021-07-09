using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.Business
{
    public class BDonation
    {
        public static List<MDonation> List(MDonation ent, ref int Val)
        {
            return DADonation.Lis(ent, ref Val);
        }
    }
}
