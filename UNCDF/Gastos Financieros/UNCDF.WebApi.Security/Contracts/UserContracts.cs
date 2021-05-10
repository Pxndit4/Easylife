using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Models;

namespace UNCDF.WebApi.Security
{
    #region Request
    
    [Serializable]
    public class UserRequest : BaseRequest
    {
        public MUser User { get; set; }
    }

    #endregion


    #region Response

    [Serializable]
    public class UserResponse : BaseResponse
    {
        public MUser User { get; set; }
    }

    [Serializable]
    public class UsersResponse : BaseResponse
    {
        public MUser[] Users { get; set; }
    }

    #endregion
}
