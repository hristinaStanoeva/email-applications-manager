using EMS.Data.Enums;
using System;

namespace EMS.Services.dto_Models
{
    public class LogDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid EmailId { get; set; }
        public DateTime LastStatusChange { get; set; }
        public EmailStatus NewStatus { get; set; }
        public EmailStatus OldStatus { get; set; }
        public EmailDto Email { get; set; }
        public UserDto User { get; set; }
    }
}