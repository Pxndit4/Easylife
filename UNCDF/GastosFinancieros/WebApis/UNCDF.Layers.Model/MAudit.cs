using System;
namespace UNCDF.Layers.Model
{
    public class MAudit
    {
        public int AuditId { get; set; }
        public string Table { get; set; }
        public int RegisterId { get; set; }
        public int Action { get; set; }
        public DateTime ActionDate { get; set; }
        public int UserId { get; set; }
    }
}
