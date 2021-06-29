using System;
using System.Collections.Generic;
using System.Text;
using UNCDF.Layers.DataAccess;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.Business
{
    public class BAudit
    {
        public static int RecordAudit(string Table, int RegisterId, int Action, int UserId)
        {
            int Val = 0;
            MAudit auditBE = new MAudit();
            auditBE.Table = Table;
            auditBE.RegisterId = RegisterId;
            auditBE.Action = Action; //1=Register 2=Update 3=Delete
            auditBE.UserId = UserId;
            DAAudit.Insert(auditBE, ref Val);

            return Val;
        }
    }
}
