using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UNCDF.Layers.Business
{
    public static class BUtilities
    {
        

        

        //public static string EncryptString(string _StringToEncrypt)
        //{
        //    string result = string.Empty;
        //    byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_StringToEncrypt);
        //    result = Convert.ToBase64String(encryted);
        //    return result;
        //}

        ///// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        //public static string DecryptString(string _StringToDecrypt)
        //{
        //    string result = string.Empty;
        //    byte[] decryted = Convert.FromBase64String(_StringToDecrypt);
        //    //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        //    result = System.Text.Encoding.Unicode.GetString(decryted);
        //    return result;
        //}
    }
}
