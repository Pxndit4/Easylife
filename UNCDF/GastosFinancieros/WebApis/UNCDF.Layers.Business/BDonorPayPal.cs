using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDonorPayPal
    {
        public static int Insert(MDonorPayPal ent, BaseRequest baseRequest)
        {

            int Result = DADonorPayPal.Insert(ent, baseRequest);

            return Result;

        }
    }
}
