using EMS.Data.dbo_Models;
using EMS.Services.Factories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace EMS.Services.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly UserManager<UserDomain> _userManager;

        public UserFactory(UserManager<UserDomain> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateUser(string username, string password, string role)
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
}
