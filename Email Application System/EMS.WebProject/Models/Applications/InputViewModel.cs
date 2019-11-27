using EMS.Data;
using System.ComponentModel.DataAnnotations;

namespace EMS.WebProject.Models.Applications
{
    public class InputViewModel
    {
        public string EmailId { get; set; }

        [Required(ErrorMessage = Constants.EnterName)]
        [StringLength(50, ErrorMessage = Constants.NameTooLong)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.EnterEGN)]
        [StringLength(10, ErrorMessage = Constants.EGNTooLong, MinimumLength = 10)]
        public string EGN { get; set; }

        [Required(ErrorMessage = Constants.EnterPhoneNumber)]
        [StringLength(25, ErrorMessage = Constants.PhoneNumberTooLong)]
        public string Phone { get; set; }
    }
}
