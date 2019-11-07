using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class UserService : IUserService
    {
        private readonly SystemDataContext _context;
        private readonly UserManager<DboUser> _userManager;

        public UserService(SystemDataContext context, UserManager<DboUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task ChangePasswordAsync(DtoUser user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<DtoUser> CreateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoUser> FindUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.MapToDtoModel();
        }
    }
}
