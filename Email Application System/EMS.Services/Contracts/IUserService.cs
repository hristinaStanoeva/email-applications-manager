using EMS.Services.dto_Models;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IUserService
    {
        Task<DtoUser> FindUserAsync(string usermane);
        Task<DtoUser> CreateAsync(string username, string password);
        Task ChangePasswordAsync(DtoUser user, string newPassword);
    }
}
