using EMS.WebProject.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Emails
{
    public class GenericEmailViewModel
    {
        public GenericAppViewModel AppViewModel { get; set; }

        public string Id { get; set; }

        public string DateReceived { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string EmailBody { get; set; }

        public string Subject { get; set; }

        public string Status { get; set; }

        public string TimeSinceCurrentStatus { get; set; }

        public bool HasAttachments { get; set; }        

        public List<string> Attachments { get; set; }
    }
}
