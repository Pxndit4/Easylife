using HiQPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BGender
    {
        public static List<MGender> List(MGender ent, ref int Val, string Language)
        {
            return DAGender.Lis(ent, ref Val, Language);

        }

        public static int Update(MGender ent, ref int Val)
        {
            return DAGender.Update(ent, ref Val);

        }

        public static MGender Select(MGender ent, ref int Val)
        {
            return DAGender.Select(ent, ref Val);
        }

        public static int Insert(MGender ent, ref int Val)
        {
            return DAGender.Insert(ent, ref Val);

        }


        public static int Delete(MGender ent, ref int Val)
        {
            return DAGender.Delete(ent, ref Val);
        }
    }
}
