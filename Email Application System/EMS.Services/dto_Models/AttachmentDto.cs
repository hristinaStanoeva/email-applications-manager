using System;

namespace EMS.Services.dto_Models
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public Guid EmailId { get; set; }
        public string Name { get; set; }
        public double SizeMb { get; set; }
        public EmailDto Email { get; set; }
    }
}