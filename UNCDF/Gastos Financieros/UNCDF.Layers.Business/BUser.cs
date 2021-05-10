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
    }
}
