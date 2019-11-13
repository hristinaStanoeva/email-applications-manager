using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class UserService : IUserService
    {
        private readonly SystemDataContext _context;
        private readonly UserManager<UserDomain> _userManager;

        public UserService(SystemDataContext context, UserManager<UserDomain> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task ChangePasswordAsync(UserDto user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(string username, string password, string role)
        {
            if (_context.Users.Any(user => user.UserName == username))
            {
                // To implement properly
                throw new ArgumentException("This user already exists");
            }
            else
            {
                var newUser = new UserDomain
                {
                    UserName = username,
                    Email = username,
                    CreatedOn = DateTime.UtcNow,
                    IsPasswordChanged = false
                };

                var result = await _userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(newUser, password);
                    await _userManager.AddToRoleAsync(newUser, role);
                    await _userManager.AddClaimsAsync(newUser, new List<Claim>()
                    {
                        new Claim("Role", role),
                        new Claim("IsPasswordChanged", newUser.IsPasswordChanged.ToString())
                    });
                }
            }
        }

        public async Task<UserDto> FindUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.MapToDtoModel();
        }
    }
}
