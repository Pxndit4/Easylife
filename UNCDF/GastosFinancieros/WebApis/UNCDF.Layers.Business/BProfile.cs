using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.Model;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public class BProfile
    {
        

        public static List<MProfile> LisByUser(MProfile ent, ref int Val)
        {
            return DAProfile.LisByUser(ent, ref Val);
        }

        public static List<MProfile> Lis(MProfile ent, ref int Val)
        {
            return DAProfile.Lis(ent, ref Val);
        }

        public static int ValidateUserPM(MUser ent)
        {
            return DAProfile.ValidateUserPM(ent);
        }

        public static int Insert(MProfile ent, List<MProfileOptions> entOpcs, ref int ProfileId)
        {
            int result = DAProfile.Insert(ent, ref ProfileId);

            if (result.Equals(0))
            {
                foreach (MProfileOptions opc in entOpcs)
                {
                    opc.ProfileId = ProfileId;
                    result = DAProfile.InsertOptions(opc);
                }
            }

            return result;
        }

        public static int Update(MProfile ent, List<MProfileOptions> entOpcs)
        {
            int result = DAProfile.Update(ent);

            if (result.Equals(0))
            {
                result = DAProfile.DeleteOptions(ent);

                if (result.Equals(0))
                {
                    foreach (MProfileOptions opc in entOpcs)
                    {
                        opc.ProfileId = ent.ProfileId;
                        result = DAProfile.InsertOptions(opc);
                    }
                }
            }

            return result;
        }

        public static MProfile Sel(MProfile ent, ref int Val)
        {
            return DAProfile.Sel(ent, ref Val);
        }

        public static List<MProfileOptions> SelOptions(MProfile ent, ref int Val)
        {
            return DAProfile.SelOptions(ent, ref Val);
        }

        public static int Delete(MProfile ent, ref int Val)
        {
            return DAProfile.Delete(ent, ref Val);
        }

        public static List<MProfileUser> LisUsers(MProfile ent, ref int Val)
        {
            return DAProfile.LisUsers(ent, ref Val);
        }

        public static List<MProfileUser> LisUsersUnAssigned(MProfileUser ent, ref int Val)
        {
            return DAProfile.LisUsersUnAssigned(ent, ref Val);
        }

        public static int InsertUser(MProfileUser ent)
        {
            return DAProfile.InsertUser(ent);
        }

        public static int DeleteUser(MProfileUser ent)
        {
            return DAProfile.DeleteUser(ent);
        }
    }
}
