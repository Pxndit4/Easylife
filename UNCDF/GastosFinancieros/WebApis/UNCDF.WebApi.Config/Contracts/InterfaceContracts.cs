using System;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class InterfaceResponse : BaseResponse
    {
        public MInterface[] Interfaces { get; set; }
    }


    [Serializable]
    public class MInterfaceResponse : BaseResponse
    {
        public MInterface Interface { get; set; }
    }

    [Serializable]
    public class InterfaceControlRequest : BaseRequest
    {
        public MInterfaceControl MInterfaceControl { get; set; }
    }

    [Serializable]
    public class InterfaceRequest : BaseRequest
    {
        public MInterface Interface { get; set; }
    }

    [Serializable]
    public class InterfaceControlResponse : BaseResponse
    {
        public MInterfaceControl InterfaceControl { get; set; }
    }

    [Serializable]
    public class InterfaceControlsResponse : BaseResponse
    {
        public MInterfaceControl[] InterfaceControls { get; set; }
    }

    [Serializable]
    public class InterfaceControlTranslateResponse : BaseResponse
    {
        public MInterfaceControlTranslate InterfaceControlTranslate { get; set; }
    }

    [Serializable]
    public class InterfaceControlTranslatesResponse : BaseResponse
    {
        public MInterfaceControlTranslate[] InterfaceControlTranslates { get; set; }
    }

    [Serializable]
    public class InterfaceControlTranslateRequest : BaseRequest
    {
        public MInterfaceControlTranslate MInterfaceControlTranslate { get; set; }
    }
}
