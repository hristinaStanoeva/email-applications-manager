using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.WebProject.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Roles = "manager, operator")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<UserDomain> _signInManager;
        private readonly ILogger<EmailController> _logger;

        public UserController(IUserService userService, SignInManager<UserDomain> signInManager, ILogger<EmailController> logger)
        {
            _userService = userService;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                var listRoles = new List<SelectListItem> {
                    new SelectListItem { Text = Constants.SelListTextManager, Value = Constants.SelListValueManager },
                    new SelectListItem { Text = Constants.SelListTextOperator, Value = Constants.SelListValueOperator }
                };

                var viewModel = new RegisterUserViewModel
                {
                    Roles = listRoles
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                TempData[Constants.TempDataMsg] = Constants.ErrorCatch;

                return View();
            }

        }

        [BindProperty]
        public RegisterUserViewModel Input { get; set; }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel vm)
        {
            try
            {
                //TODO : Do we need this if
                if (ModelState.IsValid)
                {
                    await _userService.CreateAsync(vm.Email, vm.Password, vm.Role);
                    _logger.LogInformation(string.Format(Constants.LogUserCreate, User.Identity.Name, vm.Role, vm.Email));

                    TempData[Constants.TempDataMsg] = Constants.UserCreateSucc;
                }
            }
            catch (Exception ex)
            {
                TempData[Constants.TempDataMsg] = Constants.ErrorCatch;

                _logger.LogError(ex.Message);
            }

            return RedirectToAction(Constants.PageIndex, Constants.PageHome);

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            try
            {
                await _userService.ChangePasswordAsync(vm.Username, vm.CurrentPassword, vm.Password);
                _logger.LogInformation(string.Format(Constants.LogUserPassChange, User.Identity.Name));

                TempData[Constants.TempDataMsg] = Constants.UserPassChangeSucc;

                var userName = User.Identity.Name;
                await _signInManager.SignOutAsync();
                _logger.LogInformation(string.Format(Constants.LogUserSignOut, userName));

                TempData[Constants.TempDataMsg] = Constants.UserSignOutSucc;
            }
            catch (Exception ex)
            {
                TempData[Constants.TempDataMsg] = Constants.ErrorCatch;

                _logger.LogError(ex.Message);
            }

            return LocalRedirect(Constants.ChangePassRedirect);
        }
    }
}