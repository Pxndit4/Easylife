using System.Collections.Generic;
using UNCDF.Layers.Model;
using UNCDF.Layers.DataAccess;


namespace UNCDF.Layers.Business
{
    public class BUserProject
    {
        public static int Insert(MUserProject ent)
        {
            return DAUserProject.Insert(ent);
        }

        public static List<MUserProject> List(MUserProject ent, ref int Val)
        {
            return DAUserProject.List(ent, ref Val);
        }

        public static int Delete(MUserProject ent)
        {
            return DAUserProject.Delete(ent);
        }

        public static List<MUserProject> ListAssigned(MUserProject ent, ref int Val)
        {
            return DAUserProject.ListAssigned(ent, ref Val);
        }
    }
}
