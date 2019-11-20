using EMS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMS.WebProject.Models.Users
{
    public class RegisterUserViewModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = Constants.EmailDisplayName)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = Constants.PassDisplayName)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Constants.PassConfirm)]
        [Compare(Constants.PassDisplayName, ErrorMessage = Constants.PassMatchMsg)]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem> Roles { get; set; }
        public string Role { get; set; }
    }
}
