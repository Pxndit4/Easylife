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

        public static List<MProject> List(MProject ent)
        {
            return DAProject.List(ent);
        }
    }
}
