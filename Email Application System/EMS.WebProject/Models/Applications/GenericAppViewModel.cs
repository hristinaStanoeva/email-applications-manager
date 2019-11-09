using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Applications
{
    public class GenericAppViewModel
    {
        public string Id { get; set; }

        public string EmailDateReceived { get; set; }

        public string SenderEmail { get; set; }

        public string Subject { get; set; }

        public string SenderName { get; set; }

        public string Status { get; set; }
        public string ClosedByOperator { get; set; }
    }
}
