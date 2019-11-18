using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Emails
{
    public class GenericEmailViewModel
    {
        public string Id { get; set; }

        public string DateReceived { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string Subject { get; set; }

        public string Status { get; set; }

        public string TimeSinceCurrentStatus { get; set; }

        public bool HasAttachments { get; set; }

        public string MessageId { get; set; }

        public List<AttachmentViewModel> Attachments { get; set; }
    }
}
