using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public class BAplication
    {
        public static bool ValidateAplicationToken(string Token)
        {
            if (DAAplication.ValidateAplicationToken(Token).Equals(1)) return true;
            else return false;
        }
        public static string GenerateGuid()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }
    }
}
