using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Business
{
    public class BSession
    {
        public static int ValidateSession(int Type, string Token, int UserId)
        {
            return DA.SessionDA.ValidateSession(Type, Token, UserId);
        }
    }
}
