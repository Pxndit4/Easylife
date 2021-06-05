using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNCDF.CMS.Models
{
    public class ProfileViewModel
    {
        public int ProfileId { get; set; }

        [Display(Name = "Profile")]
        //[MinLength(3, ErrorMessage = "Perfil, Minimo 3 caracteres")]
        public string Profile { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "")]
        public List<ResulOptionProfileViewModel> Result { get; set; }
        public ResulOptionProfileViewModel line { get; set; }
        public List<string> CheckOptions { get; set; }
        public string SelOptions { get; set; }

    }

    public class ResulOptionProfileViewModel
    {
        [Display(Name = "Profile Id")]
        public int ProfileId { get; set; }
        [Display(Name = "Option Id")]
        public int OptionId { get; set; }
        [Display(Name = "Flag")]
        public int FlagActive { get; set; }
        [Display(Name = "Option")]
        public string Title { get; set; }
        [Display(Name = "Sub Module")]
        public string TitleSubModule { get; set; }
        [Display(Name = "Module")]
        public string TitleModule { get; set; }
        public string FlagCheck { get; set; }
    }

    public class SearchProfileViewModel
    {
        [Display(Name = "Profile")]
        //[MinLength(3, ErrorMessage = "Perfil, Minimo 3 caracteres")]
        public string Profile { get; set; }

        [Display(Name = "")]
        public SearchProfileResultadoViewModel Result { get; set; }
    }

    public class SearchProfileResultadoViewModel
    {
        [Display(Name = "Id")]
        public int ProfileId { get; set; }

        [Display(Name = "Profile")]
        public string Description { get; set; }
    }


    public class AddUserViewModel
    {
        public int ProfileId { get; set; }

        [Display(Name = "Profile")]
        public string Description { get; set; }

        public string Status { get; set; }

        [Display(Name = "")]
        public List<ResultUserProfileViewModel> Result { get; set; }

        public ResultUserProfileViewModel line { get; set; }
    }

    public class ResultUserProfileViewModel
    {
        [Display(Name = "Profile Id")]
        public int ProfileId { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "User")]
        public string User { get; set; }
        [Display(Name = "user Name")]
        public string Name { get; set; }

    }

    public class SearchUserProfileViewModel
    {

        [Display(Name = "User")]
        public string User { get; set; }
        [Display(Name = "User name")]
        public string Name { get; set; }
        public int ProfileId { get; set; }

        public string Filter { get; set; }

        public ResultSearchUserProfileViewModelViewModel Result { get; set; }
    }

    public class ResultSearchUserProfileViewModelViewModel
    {
        [Display(Name = "Profile Id")]
        public int ProfileId { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "User")]
        public string User { get; set; }
        [Display(Name = "User Name")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}