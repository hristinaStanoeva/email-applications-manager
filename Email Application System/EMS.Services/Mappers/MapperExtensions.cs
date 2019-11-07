using EMS.Data.dbo_Models;
using EMS.Services.dto_Models;

namespace EMS.Services.Mappers
{
    public static class MapperExtensions
    {
        public static DtoUser MapToDtoModel(this DboUser user)
        {
            return new DtoUser
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
