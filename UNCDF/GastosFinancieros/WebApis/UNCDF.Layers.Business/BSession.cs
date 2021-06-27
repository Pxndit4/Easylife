using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;

namespace UNCDF.Layers.Business
{
    public class BSession
    {
        public static int ValidateSession(int Type, string Token, int UserId)
        {
            return DASession.ValidateSession(Type, Token, UserId);
        }
    }
}
