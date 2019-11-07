using EMS.Data.Enums;
using System;

namespace EMS.Services.dto_Models
{
    public class DtoLog
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid EmailId { get; set; }
        public DateTime LastStatusChange { get; set; }
        public EmailStatus NewStatus { get; set; }
        public EmailStatus OldStatus { get; set; }
        public DtoEmail Email { get; set; }
        public DtoUser User { get; set; }
    }
}