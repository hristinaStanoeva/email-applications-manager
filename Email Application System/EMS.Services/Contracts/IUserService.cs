using EMS.Services.dto_Models;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDto> FindUserAsync(string usermane);
        Task<UserDto> CreateAsync(string username, string password);
        Task ChangePasswordAsync(UserDto user, string newPassword);
    }
}
