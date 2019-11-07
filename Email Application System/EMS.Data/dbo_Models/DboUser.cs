using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EMS.Data.dbo_Models
{
    public class DboUser : IdentityUser
    {
        public DateTime CreatedOn { get; set; }
        public bool IsPasswordChanged { get; set; }

        public List<DboApplication> Applications { get; set; }
        public List<DboLog> Logs { get; set; }
    }
}
