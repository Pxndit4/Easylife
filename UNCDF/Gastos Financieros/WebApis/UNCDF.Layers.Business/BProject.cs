using System;
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
    }
}
