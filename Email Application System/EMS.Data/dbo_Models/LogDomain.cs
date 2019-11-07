using EMS.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Data.dbo_Models
{
    public class LogDomain
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

        public EmailDomain Email { get; set; }
        public UserDomain User { get; set; }
    }
}
