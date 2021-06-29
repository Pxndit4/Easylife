using System;
namespace UNCDF.Layers.Model
{
    public class MInterfaceControl
    {
        public int InterfaceControlId { get; set; }
        public int InterfaceId { get; set; }
        public string ControlName { get; set; }
        public string Description { get; set; }

        public string DescriptionControl { get; set; }
    }
}
