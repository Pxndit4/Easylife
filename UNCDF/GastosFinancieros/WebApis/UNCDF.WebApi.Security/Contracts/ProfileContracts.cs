using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Business;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Security
{
    #region Response

    [Serializable]
    public class ProfilesResponse : BaseResponse
    {
        public MProfile[] Profiles { get; set; }
    }

    [Serializable]
    public class ProfileResponse : BaseResponse
    {
        public MProfile Profile { get; set; }

        public MProfileOptions[] Options { get; set; }
    }

    [Serializable]
    public class UsersProfileResponse : BaseResponse
    {
        public MProfileUser[] UsersProfile { get; set; }
    }

    [Serializable]
    public class UserProfileResponse : BaseResponse
    {
        public MProfileUser UserProfile { get; set; }
    }

    #endregion

    #region Request

    [Serializable]
    public class ProfileRequest : BaseRequest
    {
        public MProfile Profile { get; set; }

        public MProfileOptions[] Options { get; set; }
    }

    [Serializable]
    public class ProfileUserRequest : BaseRequest
    {
        public MProfileUser ProfileUser { get; set; }

    }

    #endregion
}
