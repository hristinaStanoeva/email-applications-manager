using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Emails
{
    public class PreviewViewModel
    {
        public string Id { get; set; }

        public string DateReceived { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string EmailBody { get; set; }

        public string Subject { get; set; }
        public string Status { get; set; }

        public List<AttachmentViewModel> Attachments { get; set; }
    }
}
