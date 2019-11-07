using EMS.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Data.dbo_Models
{
    public class DboEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string GmailMessageId { get; set; }

        [Required]
        public DateTime Received { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        [Required]
        public EmailStatus Status { get; set; }
        public DateTime? ToNewStatus { get; set; }
        public DateTime? ToCurrentStatus { get; set; }
        public DateTime? ToTerminalStatus { get; set; }
        public int? NumberOfAttachments { get; set; }
        public double? SizeOfAttachmentsMb { get; set; }

        public DboApplication Application { get; set; }
        public List<DboAttachment> Attachments { get; set; }
        public List<DboLog> Logs { get; set; }
    }
}
