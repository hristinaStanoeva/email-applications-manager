using System;

namespace EMS.Services.dto_Models
{
    public class DtoAttachment
    {
        public Guid Id { get; set; }
        public Guid EmailId { get; set; }
        public string Name { get; set; }
        public double SizeMb { get; set; }
        public DtoEmail Email { get; set; }
    }
}