using System;
using System.Collections.Generic;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BProject
    {
        public static int Insert(MProject ent)
        {
            return DAProject.Insert(ent);
        }

        public static MProject Get(MProject ent)
        {
            return DAProject.Get(ent);
        }

        public static List<MProject> List(MProject ent)
        {
            return DAProject.List(ent);
        }

        public static List<MProject> ListScroll()
        {
            return DAProject.ListScroll();
        }

        public static List<MProject> ListFilter(MProject ent)
        {
            return DAProject.ListFilter(ent);
        }

        public static int Update(MProject ent )
        {
            return DAProject.Update(ent);
        }
    }
}
