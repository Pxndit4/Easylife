using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BProgramName
    {
        public static List<MProgramName> List()
        {
            return DAProgramName.List();
        }

        public static int Insert(MProgramName ent)
        {
            return DAProgramName.Insert(ent);
        }
        public static List<MProgramName> ListValidProgramName()
        {
            return DAProgramName.ListValidProgramName();
        }
        
    }
}
