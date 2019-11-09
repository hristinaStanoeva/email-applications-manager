using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Emails
{
    public class AllEmailsViewModel
    {
        public List<GenericEmailViewModel> AllEmails { get; set; }

        public string ActiveTab { get; set; }
    }
}
