using System.Collections.Generic;

namespace EMS.WebProject.Models.Emails
{
    public class AllEmailsViewModel
    {
        public List<GenericEmailViewModel> AllEmails { get; set; }

        public string ActiveTab { get; set; }
    }
}
