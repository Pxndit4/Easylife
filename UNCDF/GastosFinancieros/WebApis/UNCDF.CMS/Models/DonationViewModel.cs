using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class DonationViewModel
    {
    }

    public class SearchDonationViewModel
    {
        public int ProjectId { get; set; }

        [Display(Name = "Ong")]
        public int OngId { get; set; }

        [Display(Name = "Donor Id")]
        public int DonorId { get; set; }

        [Display(Name = "Type Donation")]
        public int TypeDonation { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public string CheckDonations { get; set; }

        [Display(Name = "Donations selected")]

        public decimal SelectedAmount { get; set; }

        public decimal SelectedAmountSend { get; set; }

        [Display(Name = "Total Donations")]

        public decimal TotalAmount { get; set; }

        [Display(Name = "")]
        public ResultSearchDonationViewModel Result { get; set; }
    }

    public class ResultSearchDonationViewModel
    {
        [Display(Name = "Donation Id")]
        public int DonationId { get; set; }

        public string Ong { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Project { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Date")]
        public string DateStr { get; set; }

        public int FlagActive { get; set; }
        public int Status { get; set; }


    }
}