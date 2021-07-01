using System;
namespace UNCDF.Layers.Model
{
    public class MInterface
    {
        public int InterfaceId { get; set; }
        public int TypeId { get; set; }
        public string InterfaceName { get; set; }
        public string ControlName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }


        //DATOS ADICIONALES PARA LA VISTA
        public string Type { get; set; }
        public string StatusName { get; set; }
    }
}
