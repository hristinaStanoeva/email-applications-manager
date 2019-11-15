using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Applications
{
    public class AppPreviewViewModel
    {
        public string Id { get; set; }

        public EmailDto Email { get; set; }

        public string EGN { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public ApplicationStatus Status { get; set; }

        public string OperatorName { get; set; }

    }
}
