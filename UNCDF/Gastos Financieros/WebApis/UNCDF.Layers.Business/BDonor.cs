using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Models;

namespace UNCDF.Layers.Business
{
    public class BDonor
    {
        public static int Insert(MDonor ent, BaseRequest baseRequest, ref int DonorId)
        {
            int Registered = 0;

            int Result = DADonor.ValidateInsert(ent.Cellphone, ent.Email, ref Registered, ref DonorId);

            ent.Registered = Registered;

            if (Result == 0)
            {
                return DADonor.Insert(ent, baseRequest);
            }
            else
            {
                return Result;
            }

        }

        public static int Update(MDonor ent)
        {
            return DADonor.Update(ent);
        }

        public static int UpdateCode(MDonor ent)
        {
            return DADonor.UpdateCode(ent);
        }

        public static MDonor ValidateDonor(MDonor ent, ref int Val)
        {
            return DADonor.ValidateDonor(ent, ref Val);
        }

        public static int ValidateCode(MDonor ent)
        {
            return DADonor.ValidateCode(ent);
        }

        public static int ChangePassword(MDonor ent)
        {
            return DADonor.ChangePassword(ent);
        }

        public static MDonor Login(MDonor ent, ref int Val)
        {
            return DADonor.Login(ent, ref Val);
        }

        public static MDonor Select(MDonor ent, ref int Val)
        {
            return DADonor.Select(ent, ref Val);
        }

        public static int Delete(MDonor ent)
        {
            return DADonor.Delete(ent);
        }

        public static List<MDonor> List(MDonor ent, ref int Val)
        {
            return DADonor.Lis(ent, ref Val);
        }
    }
}
