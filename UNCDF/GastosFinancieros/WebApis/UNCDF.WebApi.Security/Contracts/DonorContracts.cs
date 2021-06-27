using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNCDF.Layers.Model;

namespace UNCDF.WebApi.Security
{
    #region Response
    
    [Serializable]
    public class DonorsResponse : BaseResponse
    {
        public MDonor[] Donors { get; set; }
    }

    [Serializable]
    public class DonorResponse : BaseResponse
    {
        public MDonor Donor { get; set; }
        public int DonationsCounter { get; set; }
    }


    [Serializable]
    public class CreateDonorResponse : BaseResponse
    {
        public MDonor Donor { get; set; }
    }

    #endregion

    #region Region

    [Serializable]
    public class DonorRequest : BaseRequest
    {
        public MDonor Donor { get; set; }

        public byte[] FileByte { get; set; }
        public string PhotoExtension { get; set; }
    }

    #endregion
}
