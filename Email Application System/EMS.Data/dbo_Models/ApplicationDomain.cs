using EMS.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Data.dbo_Models
{
    public class ApplicationDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid EmailId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string EGN { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; }

        public EmailDomain Email { get; set; }
        public UserDomain User { get; set; }
    }
}
