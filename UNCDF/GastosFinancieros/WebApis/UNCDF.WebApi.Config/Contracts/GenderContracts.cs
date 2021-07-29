using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Config
{
    [Serializable]
    public class GenderResponse : BaseResponse
    {
        public MGender Gender { get; set; }
    }

    [Serializable]
    public class GendersResponse : BaseResponse
    {
        public MGender[] Genders { get; set; }
    }

    [Serializable]
    public class GenderRequest : BaseRequest
    {
        public MGender GenderBE { get; set; }
    }
}
