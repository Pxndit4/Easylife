using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BProjectFinancial
    {
        public static int Insert(MProjectFinancials ent)
        {
           return  DAProjectFinancial.Insert(ent);
        }
        public static int InsertHistory(MProjectFinancials ent)
        {
            return DAProjectFinancial.InsertHistory(ent);
        }
        

        public static List<MProjectFinancials> FilProjectFinancial(MProjectFinancials ent, ref int Val)
        {
            return DAProjectFinancial.FilProjectFinancial(ent,ref Val);
        }
    }
}
