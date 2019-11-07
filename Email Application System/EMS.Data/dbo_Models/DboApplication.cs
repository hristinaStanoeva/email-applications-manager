using EMS.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Data.dbo_Models
{
    public class DboApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid EmailId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string EGN { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; }

        public DboEmail Email { get; set; }
        public DboUser User { get; set; }
    }
}
