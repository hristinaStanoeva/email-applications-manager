using EMS.Data.Enums;
using System;

namespace EMS.Services.dto_Models
{
    public class DtoApplication
    {
        public Guid Id { get; set; }
        public Guid EmailId { get; set; }
        public string UserId { get; set; }
        public string EGN { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ApplicationStatus Status { get; set; }
        public DtoEmail Email { get; set; }
        public DtoUser User { get; set; }
    }
}