using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    #region Response

    [Serializable]
    public class ProjectsResponse : BaseResponse
    {
        public MProject[] Projects { get; set; }
    }

    [Serializable]
    public class ProjectResponse : BaseResponse
    {
        public MProject Project { get; set; }
    }

    [Serializable]
    public class ProjectPropertiesResponse : BaseResponse
    {
        public MProgramName[] ProgramNames { get; set; }
        public MDonorPartner[] DonorPartners { get; set; }
    }

    [Serializable]
    public class ProjectFinancialPropertiesResponse : BaseResponse
    {
        public MDeparment[] Deparments { get; set; }
        public MImplementAgency[] ImplementAgencies { get; set; }
        public MFund[] Funds { get; set; }
    }

    #endregion

    #region Request

    [Serializable]
    public class ProjectsRequest : BaseRequest
    {
        public List<MProject> Projects { get; set; }
    }

    [Serializable]
    public class ProjectRequest : BaseRequest
    {
        public MProject Project { get; set; }
    }

    #endregion    
}
