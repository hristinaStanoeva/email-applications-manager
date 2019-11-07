using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EMS.Data.dbo_Models
{
    public class UserDomain : IdentityUser
    {
        public DateTime CreatedOn { get; set; }
        public bool IsPasswordChanged { get; set; }

        public List<ApplicationDomain> Applications { get; set; }
        public List<LogDomain> Logs { get; set; }
    }
}
