using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project.Contracts
{
    public class ProjectExclusionsResponse : BaseResponse
    {
        public MProjectExclusion[] projectExclusions { get; set; }
    }
    
    public class ProjectExclusionResponse : BaseResponse
    {
        public MProjectExclusion projectExclusion { get; set; }
    }
    
    [Serializable]
    public class ProjectExclusionRequest : BaseRequest
    {
        public MProjectExclusion projectExclusion { get; set; }
    }
}
