using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDonorStripe
    {
        public static int Confirm(MDonorStripe ent, BaseRequest baseRequest)
        {

            int Result = DADonorStripe.Confirm(ent, baseRequest);

            return Result;
        }

        public static int Create(string PaymentId, string ClientSecret, Int32 donorId)
        {
            int Result = DADonorStripe.Create(PaymentId, ClientSecret, donorId);

            return Result;
        }

        public static int Cancel(string PaymentId)
        {
            int Result = DADonorStripe.Cancel(PaymentId);

            return Result;
        }
    }
}
