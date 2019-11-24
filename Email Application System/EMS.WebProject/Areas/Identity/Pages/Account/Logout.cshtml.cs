using EMS.Data;
using EMS.Data.dbo_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EMS.WebProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<UserDomain> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<UserDomain> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var userName = User.Identity.Name;

            await _signInManager.SignOutAsync();

            _logger.LogInformation(string.Format(Constants.LogUserLogout, userName));

            if (returnUrl != null)
            {
                return LocalRedirect("~/Identity/Account/Login/");
            }
            else
            {
                return Page();
            }
        }
    }
}