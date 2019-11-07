using EMS.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Data.dbo_Models
{
    public class DboLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid EmailId { get; set; }

        [Required]
        public DateTime LastStatusChange { get; set; }

        [Required]
        public EmailStatus NewStatus { get; set; }

        [Required]
        public EmailStatus OldStatus { get; set; }

        public DboEmail Email { get; set; }
        public DboUser User { get; set; }
    }
}
