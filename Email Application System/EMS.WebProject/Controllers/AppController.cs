using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.WebProject.Models.Applications;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebProject.Controllers
{
    public class AppController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;


        public AppController(IApplicationService appService, IUserService userService, IEmailService emailService)
        {
            _appService = appService;
            _userService = userService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MarkOpen(InputViewModel vm)
        {            
            var user = await _userService.FindUserAsync(User.Identity.Name);

            await _appService.MarkOpenAsync(vm.EmailId, user.Id, vm.EGN, vm.Name, vm.Phone);

            await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Open);

            return RedirectToAction("Index", "Email");
        }
    }
}