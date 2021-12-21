using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    #region Response

    [Serializable]
    public class ProjectsFinancialYearResponse : BaseResponse
    {
        public string[] Years { get; set; }
    }

    [Serializable]
    public class ProjectsFinancialResponse : BaseResponse
    {
        public MProjectFinancials Financial { get; set; }
    }

    [Serializable]
    public class ProjectsFlagsResponse : BaseResponse
    {
        public string[] Flags { get; set; }
    }

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
    public class ProjectTotalsResponse : BaseResponse
    {
        public int Projects { get; set; }
        public int Countries { get; set; }
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

    [Serializable]
    public class ProjectFinancialYearResponse : BaseResponse
    {
        public string[] Years { get; set; }
    }


    #endregion

    #region Request

    [Serializable]
    public class ProjectsRequest : BaseRequest
    {
        public int TotalBad { get; set; }
        public int TotalCorrect { get; set; }
        public List<MProject> Projects { get; set; }
    }

    [Serializable]
    public class ProjectRequest : BaseRequest
    {
        public MProject Project { get; set; }
    }

    [Serializable]
    public class ProjectFinancialRequest : BaseRequest
    {
        public MProjectFinancials ProjectFinancial { get; set; }
    }


    [Serializable]
    public class ProjectFinancialYearRequest : BaseRequest
    {
        public string Year { get; set; }
    }

    #endregion    
}
