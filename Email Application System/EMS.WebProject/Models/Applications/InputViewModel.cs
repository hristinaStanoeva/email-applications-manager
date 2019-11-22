using System.ComponentModel.DataAnnotations;

namespace EMS.WebProject.Models.Applications
{
    public class InputViewModel
    {
        public string EmailId { get; set; }

        [Required(ErrorMessage = "You have to enter a name")]
        [StringLength(50, ErrorMessage = "Name you have entered is too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to enter personal ID")]
        [StringLength(10, ErrorMessage = "EGN you have entered is not valid", MinimumLength = 10)]
        public string EGN { get; set; }

        [Required(ErrorMessage = "You have to enter phone number")]
        [StringLength(25, ErrorMessage = "Phone number you have entered is too long")]
        public string Phone { get; set; }
    }
}
