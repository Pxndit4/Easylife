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
    }
}
