using EMS.Data.Enums;
using System;
using System.Collections.Generic;

namespace EMS.Services.dto_Models
{
    public class DtoEmail
    {
        public Guid Id { get; set; }
        public string GmailMessageId { get; set; }
        public DateTime Received { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailStatus Status { get; set; }
        public DateTime? ToNewStatus { get; set; }
        public DateTime? ToCurrentStatus { get; set; }
        public DateTime? ToTerminalStatus { get; set; }
        public int? NumberOfAttachments { get; set; }
        public double? SizeOfAttachmentsMb { get; set; }
        public DtoApplication Application { get; set; }
        public List<DtoAttachment> Attachments { get; set; }
        public List<DtoLog> Logs { get; set; }
    }
}
