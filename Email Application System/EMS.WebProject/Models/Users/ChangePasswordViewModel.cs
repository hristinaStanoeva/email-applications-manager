using EMS.Data;
using System.ComponentModel.DataAnnotations;

namespace EMS.WebProject.Models.Users
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = Constants.PassDisplayName)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Constants.PassConfirm)]
        [Compare(Constants.PassDisplayName, ErrorMessage = Constants.PassMatchMsg)]
        public string ConfirmPassword { get; set; }

        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
    }
}
