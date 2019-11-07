using EMS.Data.dbo_Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Data.Seed
{
    public static class AccountSeeder
    {
        public static async Task Init(SystemDataContext context, UserManager<UserDomain> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            await SeedRoles(context, roleManager);
            await SeedManagers(context, userManager);
            await SeedOperators(context, userManager);
        }

        private static async Task SeedRoles(SystemDataContext context, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Roles.Any(role => role.Name == Constants.roleOperator))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = Constants.roleOperator
                });
            }
            if (!context.Roles.Any(role => role.Name == Constants.roleManager))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = Constants.roleManager
                });
            }
        }

        private static async Task SeedManagers(SystemDataContext context, UserManager<UserDomain> userManager)
        {
            if (!context.Users.Any(user => user.UserName == Constants.defaultManagerUsername))
            {
                var newManager = new UserDomain
                {
                    UserName = Constants.defaultManagerUsername,
                    Email= Constants.defaultManagerUsername,
                    CreatedOn = DateTime.Now,
                    IsPasswordChanged = true
                };

                var result = await userManager.CreateAsync(newManager);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(newManager, Constants.defaultPassword);
                    await userManager.AddToRoleAsync(newManager, Constants.roleManager);
                }
            }
        }

        private static async Task SeedOperators(SystemDataContext context, UserManager<UserDomain> userManager)
        {
            if (!context.Users.Any(user => user.UserName == Constants.defaultOperator1Username))
            {
                var newOperator = new UserDomain
                {
                    UserName = Constants.defaultOperator1Username,
                    Email= Constants.defaultOperator1Username,
                    CreatedOn = DateTime.Now,
                    IsPasswordChanged = false
                };

                var result = await userManager.CreateAsync(newOperator);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(newOperator, Constants.defaultPassword);
                    await userManager.AddToRoleAsync(newOperator, Constants.roleOperator);
                }
            }

            if (!context.Users.Any(user => user.UserName == Constants.defaultOperator2Username))
            {
                var newOperator = new UserDomain
                {
                    UserName = Constants.defaultOperator2Username,
                    Email = Constants.defaultOperator2Username,
                    CreatedOn = DateTime.Now,
                    IsPasswordChanged = false
                };

                var result = await userManager.CreateAsync(newOperator);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(newOperator, Constants.defaultPassword);
                    await userManager.AddToRoleAsync(newOperator, Constants.roleOperator);
                }
            }
        }
    }
}
