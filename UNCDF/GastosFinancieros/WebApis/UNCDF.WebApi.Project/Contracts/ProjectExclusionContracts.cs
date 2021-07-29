using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;


namespace UNCDF.WebApi.Project
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

    

    public class DeparmentExclusionsResponse : BaseResponse
    {
        public MDeparmentExclusion[] departementExclusions { get; set; }
    }

    public class DeparmentExclusionResponse : BaseResponse
    {
        public MDeparmentExclusion deparmentExclusion { get; set; }
    }

    [Serializable]
    public class DeparmentExclusionRequest : BaseRequest
    {
        public MDeparmentExclusion deparmentExclusion { get; set; }
    }


    public class PracticeAreasResponse : BaseResponse
    {
        public MPracticeAreaExclusion[] practiceAreaExclusions { get; set; }
    }

    public class PracticeAreaExclusionResponse : BaseResponse
    {
        public MPracticeAreaExclusion practiceAreaExclusion { get; set; }
    }

    [Serializable]
    public class PracticeAreaExclusionRequest : BaseRequest
    {
        public MPracticeAreaExclusion practiceAreaExclusion { get; set; }
    }
}
