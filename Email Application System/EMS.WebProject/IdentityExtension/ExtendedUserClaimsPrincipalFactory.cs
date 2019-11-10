using EMS.Data.dbo_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMS.WebProject.IdentityExtension
{
    public class ExtendedUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserDomain>
    {
        public ExtendedUserClaimsPrincipalFactory(
            UserManager<UserDomain> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserDomain user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("IsPasswordChanged", user.IsPasswordChanged.ToString()));
            return identity;
        }
    }
}
