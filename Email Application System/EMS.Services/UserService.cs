﻿using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Factories.Contracts;
using EMS.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class UserService : IUserService
    {
        private readonly SystemDataContext _context;
        private readonly UserManager<UserDomain> _userManager;
        private readonly IUserFactory _factory;

        public UserService(SystemDataContext context, UserManager<UserDomain> userManager, IUserFactory factory)
        {
            _context = context;
            _userManager = userManager;
            _factory = factory;
        }

        public async Task ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, currentPassword);

            if (!isPasswordCorrect)
            {
                // To implement properly
                throw new ArgumentException(Constants.UserWrongPass);
            }
            else
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (!result.Succeeded)
                {
                    // To implement properly
                    throw new ArgumentException(Constants.UserInvalidPass);
                }
                else
                {
                    user.IsPasswordChanged = true;
                    var claim = _context.UserClaims.FirstOrDefault(userclaim => userclaim.ClaimType == "IsPasswordChanged" && userclaim.UserId == user.Id);
                    claim.ClaimValue = "True";
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task CreateAsync(string username, string password, string role)
        {
            if (_context.Users.Any(user => user.UserName == username))
            {
                // To implement properly
                throw new ArgumentException(Constants.UserExists);
            }
            else
            {
                await _factory.CreateUser(username, password, role);
            }
        }

        public async Task<UserDto> FindUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.MapToDtoModel();
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.Id;
        }
    }
}
