using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BProjectDonation
    {
        public static List<MProjectDonation> List(MDonor ent, BaseRequest baseRequest)
        {
            return DAProjectDonation.List(ent, baseRequest);
        }
    }
}
