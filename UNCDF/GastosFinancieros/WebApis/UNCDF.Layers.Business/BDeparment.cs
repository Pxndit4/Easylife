using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BDeparment
    {
        public static List<MDeparment> List()
        {
            return DADeparment.List();
        }

        public static MDeparment Get(MDeparment ent)
        {
            return DADeparment.Get(ent);
        }

        public static int Insert(MDeparment ent)
        {
            return DADeparment.Insert(ent);
        }

        public static int Update(MDeparment ent)
        {
            return DADeparment.Update(ent);

        }
    }
}
