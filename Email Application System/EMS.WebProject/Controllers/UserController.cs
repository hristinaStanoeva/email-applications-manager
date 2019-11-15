using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.WebProject.Areas.Identity.Pages.Account;
using EMS.WebProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<UserDomain> _signInManager;
        private readonly UserManager<UserDomain> _userManager;

        public UserController(UserManager<UserDomain> userManager, IUserService userService, SignInManager<UserDomain> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var listRoles = new List<SelectListItem> {
                new SelectListItem { Text = "Manager", Value = "manager" },
                new SelectListItem { Text = "Operator", Value = "operator" }
            };

            var viewModel = new RegisterUserViewModel
            {
                Roles = listRoles
            };

            return View(viewModel);
        }

        [BindProperty]
        public RegisterUserViewModel Input { get; set; }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(viewModel.Email, viewModel.Password, viewModel.Role);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            await _userService.ChangePasswordAsync(viewModel.Username, viewModel.CurrentPassword, viewModel.Password);
            await _signInManager.SignOutAsync();
            return LocalRedirect("/Identity/Account/Login");
        }
    }
}