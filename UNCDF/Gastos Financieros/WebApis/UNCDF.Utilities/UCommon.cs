using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UNCDF.Utilities
{
    public class UCommon
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string GetTokem()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 30).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
