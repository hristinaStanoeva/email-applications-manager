using System;

namespace EMS.Services.dto_Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPasswordChanged { get; set; }
    }
}
