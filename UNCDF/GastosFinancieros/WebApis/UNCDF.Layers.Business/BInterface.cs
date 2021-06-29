using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BInterface
    {
        public static List<MInterface> List(MInterface ent, BaseRequest baseRequest)
        {
            return DAInterface.List(ent, baseRequest);
        }
        public static int Insert(MInterface ent, ref int Val)
        {
            return DAInterface.Insert(ent, ref Val);

        }

        public static int Update(MInterface ent, ref int Val)
        {
            return DAInterface.Update(ent, ref Val);

        }

        public static MInterface Select(MInterface ent, ref int Val)
        {
            return DAInterface.Select(ent, ref Val);
        }

        public static List<MInterface> Filter(MInterface ent, ref int Val)
        {
            return DAInterface.Filter(ent, ref Val);
        }

        public static int Delete(MInterface ent, ref int Val)
        {
            return DAInterface.Delete(ent, ref Val);
        }
    }
}
