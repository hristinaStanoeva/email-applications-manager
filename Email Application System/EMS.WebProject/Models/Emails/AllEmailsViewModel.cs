using EMS.WebProject.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Models.Emails
{
    public class AllEmailsViewModel
    {
        public List<GenericEmailViewModel> AllEmails { get; set; }

        public List<GenericAppViewModel> AllApps { get; set; }

        public string ActiveTab { get; set; }
    }
}
