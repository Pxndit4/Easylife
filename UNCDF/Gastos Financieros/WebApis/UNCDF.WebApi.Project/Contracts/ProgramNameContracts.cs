﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Project
{
    #region Response

    [Serializable]
    public class ProgramNamesResponse : BaseResponse
    {
        public MProgramName[] ProgramNames { get; set; }
    }

    #endregion

    [Serializable]
    public class ProgramNamesRequest : BaseRequest
    {
        public MProgramName[] ProgramNames { get; set; }
    }
}
