using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data.Enums;
using EMS.Data.Seed;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
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

        public async Task<IActionResult> MarkAppNew(string id)
        {
            var emailId = await _appService.FindAsync(id);

            await _appService.Delete(id);

            await _emailService.ChangeStatusAsync(emailId.ToString(), EmailStatus.New);

            TempData["message"] = Constants.SuccAppNew;

            return RedirectToAction("Index", "Email");
        }

        public async Task<IActionResult> Preview(string id)
        {
            var appByEmailId = await _appService.GetByMailIdAsync(id);

            var vm = appByEmailId.MapToViewModelPreview(id);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAppStatus(AppPreviewViewModel vm)
        {
            string str = Request.Form["submitbtn"];
            if (str == "valid")
            {
                await _appService.ChangeStatusAsync(vm.Id, ApplicationStatus.Approved);
            }
            else if (str == "invalid")
            {
                await _appService.ChangeStatusAsync(vm.Id, ApplicationStatus.Rejected);
            }

            await _emailService.ChangeStatusAsync(vm.Email.Id.ToString(), EmailStatus.Closed);

            TempData["message"] = Constants.SuccAppValid;

            return RedirectToAction("Index", "Email");
        }

        public async Task<IActionResult> MarkInvalid(AppPreviewViewModel vm)
        {
            await _appService.ChangeStatusAsync(vm.Id, ApplicationStatus.Rejected);

            await _emailService.ChangeStatusAsync(vm.Email.Id.ToString(), EmailStatus.Closed);

            TempData["message"] = Constants.SuccAppInvalid;

            return RedirectToAction("Index", "Email");
        }

        public async Task<IActionResult> MarkOpen(InputViewModel vm)
        {
            var user = await _userService.FindUserAsync(User.Identity.Name);

            await _appService.CreateAsync(vm.EmailId, user.Id, vm.EGN, vm.Name, vm.Phone);

            await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Open);

            TempData["message"] = Constants.SuccAppCreate;

            return RedirectToAction("Index", "Email");
        }
    }
}