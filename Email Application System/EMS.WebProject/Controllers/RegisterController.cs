using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.WebProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMS.WebProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SignInManager<UserDomain> _signInManager;
        private readonly IUserService _userService;
        private readonly UserManager<UserDomain> _userManager;
        //private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        public RegisterController(UserManager<UserDomain> userManager, SignInManager<UserDomain> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
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
        public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(viewModel.Email, viewModel.Password, viewModel.Role);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}