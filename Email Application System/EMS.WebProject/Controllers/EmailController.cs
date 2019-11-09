using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Applications;
using EMS.WebProject.Models.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebProject.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;
        private static List<GenericEmailViewModel> _allEmails;

        public EmailController(IEmailService emailService, IApplicationService appService)
        {
            _emailService = emailService;
            _appService = appService;
        }
        public async Task<IActionResult> Index()
        {
            var emailsIndex = await _emailService.GetAllEmailsAsync();

            var vm = new AllEmailsViewModel
            {
                AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            _allEmails = vm.AllEmails;

            return View("Index", vm);
        }

        public IActionResult NewEmails()
        {
            var vm = new AllEmailsViewModel
            {
                AllEmails = _allEmails.Where(x => x.Status == EmailStatus.New.ToString()).ToList(),
                ActiveTab = "new"
            };

            return View("Index", vm);
        }

        public IActionResult OpenEmails()
        {
            var vm = new AllEmailsViewModel
            {
                AllEmails = _allEmails.Where(x => x.Status == EmailStatus.Open.ToString()).ToList(),
                ActiveTab = "open"
            };

            return View("Index", vm);
        }

        public IActionResult ClosedEmails()
        {
            var apps = _appService.GetAllAppsAsync();

            var vm = new AllEmailsViewModel
            {
                AllApps = apps.Select(x => x.MapToViewModel()).ToList(),
                AllEmails = _allEmails.Where(x => x.Status == EmailStatus.Closed.ToString()).ToList(),
                ActiveTab = "closed"
            };

            return View("Index", vm);
        }

        public async Task<IActionResult> MakeInvalid(string id)
        {
            await _emailService.MakeInvalidAsync(id);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var vm = new AllEmailsViewModel
            {
                AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            return View("Index", vm);
        }

        public async Task<IActionResult> RestoreInvalid(string id)
        {
            await _emailService.RestoreInvalidAsync(id);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var vm = new AllEmailsViewModel
            {
                AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            return View("Index", vm);
        }

        [HttpGet]
        public IActionResult Preview(string id)
        {
            var email = _allEmails.FirstOrDefault(x => x.Id.ToString() == id);
            var vm = new GenericEmailViewModel
            {
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                EmailBody = email.EmailBody
            };

            return View(vm);
        }
    }
}