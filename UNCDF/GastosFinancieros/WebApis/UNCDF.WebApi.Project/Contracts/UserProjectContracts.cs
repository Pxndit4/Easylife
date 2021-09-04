using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    [Serializable]
    public class UserProjectsResponse : BaseResponse
    {
        public MUserProject[] UserProjects { get; set; }
    }

    [Serializable]
    public class UserProjectResponse : BaseResponse
    {
        public MUserProject UserProject { get; set; }
    }


    [Serializable]
    public class UserProjectRequest : BaseRequest
    {
        public MUserProject UserProject { get; set; }

    }
}
