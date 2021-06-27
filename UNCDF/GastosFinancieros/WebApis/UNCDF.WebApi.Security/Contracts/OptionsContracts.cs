using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Security
{
    #region Response
    [Serializable]
    public class OptionsResponse : BaseResponse
    {
        public MOptions[] Options { get; set; }
    }
    #endregion

    #region Request

    [Serializable]
    public class OptionsRequest : BaseRequest
    {
        public MOptions Options { get; set; }
    }

    #endregion
}
