using System;
using System.Collections.Generic;

namespace EMS.GmailAPI.gmail_Models
{
    public class EmailGmail
    {
        public string GmailMessageId { get; set; }
        public DateTime Received { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<AttachmentGmail> Attachments { get; set; }
    }
}
