using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class DonorPartnerViewModel
    {
        public byte[] FileByte { get; set; }
        public string File { get; set; }
        [Display(Name = "Donor Code")]
        public string DonorCode { get; set; }
        public string DonorName { get; set; }
        public string FundingPartner { get; set; }
        public ResultSearchDonorParnertViewModel result { get; set; }
    }

    public class ResultSearchDonorParnertViewModel
    {
        public int DonorPartnerId { get; set; }
        [Display(Name = "Donor Code")]
        public string DonorCode { get; set; }
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; }
        [Display(Name = "Funding Partner")]
        public string FundingPartner { get; set; }
        [Display(Name = "Donor Long Description")]
        public string DonorLongDescription { get; set; }
    }

    public class LoadDonorPartnersViewModel
    {
        public byte[] FileByte { get; set; }
        [Required]
        public string File { get; set; }

        public ResultLoadDonorPartnersViewModel resulLoad { get; set; }
    }

    public class ResultLoadDonorPartnersViewModel
    {
        public int DonorPartnerId { get; set; }
        [Display(Name = "Donor Code")]
        public string DonorCode { get; set; }
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; }
        [Display(Name = "Funding Partner")]
        public string FundingPartner { get; set; }
        [Display(Name = "Donor Long Description")]
        public string DonorLongDescription { get; set; }
        
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }

    public class ModelDonorPartnerResult : MDonorPartner
    {
        public string AlertMessage { get; set; }
        public string WithAlert { get; set; }
    }
}