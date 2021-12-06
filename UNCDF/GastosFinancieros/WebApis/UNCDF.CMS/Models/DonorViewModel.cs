using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class SearchDonorViewModel
    {
        public int DonorId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public int Status { get; set; }
        public int Registered { get; set; }

        [Display(Name = "")]
        public ResultSearchDonorViewModel Result { get; set; }
    }

    public class ResultSearchDonorViewModel
    {
        public int DonorId { get; set; }
        public string Name { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Cell phone")]
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public int Status { get; set; }
        [Display(Name = "Status")]
        public string StatusName { get; set; }
        public int Registered { get; set; }
        [Display(Name = "Registered")]
        public string RegisteredName { get; set; }
        [Display(Name = "Donated")]
        public int Donated { get; set; }




    }
}