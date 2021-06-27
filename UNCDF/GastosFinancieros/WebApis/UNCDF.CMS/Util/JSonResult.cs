using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNCDF.CMS
{
    public class JSonResult
    {
        public const string Error = "error";
        public const string Warning = "warning";
        public const string Invalid = "invalid";
        public const string Ok = "ok";

        public object data { get; set; }
        public string message { get; set; }
        public bool isError { get; set; }
        public string type { get; set; }
        public Nullable<int> IdPrincipal { get; set; }
    }
}