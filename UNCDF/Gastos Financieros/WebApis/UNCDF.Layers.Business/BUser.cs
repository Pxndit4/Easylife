using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.Models;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public class BUser
    {
        public static int Insert(MUser ent, ref int Val)
        {
            return DUser.Insert(ent, ref Val);
        }

        public static int Update(MUser ent, ref int Val)
        {
            return DUser.Update(ent, ref Val);
        }

        public static List<MUser> Lis(MUser ent, ref int Val)
        {
            return DUser.Lis(ent, ref Val);
        }

        public static int ChangePassword(MUser ent, ref int Val)
        {
            return DUser.ChangePassword(ent, ref Val);
        }

        public static MUser Sel(MUser ent, ref int Val)
        {
            return DUser.Sel(ent, ref Val);
        }

        public static int Delete(MUser ent, ref int Val)
        {
            return DUser.Delete(ent, ref Val);
        }
    }
}
