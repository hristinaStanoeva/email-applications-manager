using EMS.Services.dto_Models;
using EMS.WebProject.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Applications
{
    public class GenericAppViewModel
    {
        public GenericEmailViewModel EmailViewModel { get; set; }

        public string Id { get; set; }     

        public string EGN { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Status { get; set; }

        public UserDto Operator { get; set; }

        public EmailDto Email { get; set; }
    }
}
