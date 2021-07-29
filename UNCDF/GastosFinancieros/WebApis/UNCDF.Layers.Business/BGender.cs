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
    }
}
