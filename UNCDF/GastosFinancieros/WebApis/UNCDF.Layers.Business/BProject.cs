using System;
using System.Collections.Generic;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BProject
    {
        public static MProjectFinancials GetFinancials(MProjectFinancials ent, ref int Val)
        {
            return DAProject.GetFinancials(ent, ref Val);
        }

        public static MProjectFinancials GetFinancialsByYear(MProjectFinancials ent, ref int Val)
        {
            return DAProject.GetFinancialsByYear(ent, ref Val);
        }

        public static List<MProject> ListGroupbyCountry(MProject ent)
        {
            return DAProject.ListGroupbyCountry(ent);
        }

        public static List<MProject> ListbyCountry(MProject ent)
        {
            return DAProject.ListbyCountry(ent);
        }

        public static List<String> YearLis(MProjectFinancials ent, ref int Val)
        {
            return DAProject.YearLis(ent, ref Val);
        }
        public static int Insert(MProject ent)
        {
            return DAProject.Insert(ent);
        }

        public static List<String> GetFlags(ref int Val)
        {
            return DAProject.GetFlags(ref Val);
        }
        public static List<MProject> RandomLis(BaseRequest baseRequest, ref int Val)
        {
            return DAProject.RandomLis(baseRequest, ref Val);
        }

        public static MProject Get(MProject ent)
        {
            return DAProject.Get(ent);
        }

        public static MProject GetDetails(MProject ent, ref int val)
        {
            return DAProject.GetDetails(ent, ref val);
        }

        public static List<MProject> List(MProject ent,Session session)
        {
            return DAProject.List(ent, session);
        }

        public static List<MProject> ListScroll()
        {
            return DAProject.ListScroll();
        }

        public static List<MProject> ListFilter(MProject ent)
        {
            return DAProject.ListFilter(ent);
        }

        public static int Update(MProject ent)
        {
            return DAProject.Update(ent);
        }

        public static List<MProject> ListProjectCodeExclusions(MProject ent)
        {
            return DAProject.ListProjectCodeExclusions(ent);
        }
    }
}
