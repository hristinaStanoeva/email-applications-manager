using EMS.Data.Enums;

namespace EMS.WebProject.Models.Applications
{
    public class AppPreviewViewModel
    {
        public string Id { get; set; }

        public string EGN { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public ApplicationStatus Status { get; set; }

        public string OperatorName { get; set; }
    }
}
