using EMS.Data.dbo_Models;
using EMS.Services.dto_Models;

namespace EMS.Services.Mappers
{
    public static class MapperExtensions
    {
        public static UserDto MapToDtoModel(this UserDomain user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                IsPasswordChanged = user.IsPasswordChanged
            };
        }
    }
}
