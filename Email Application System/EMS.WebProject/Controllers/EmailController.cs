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
using GmailAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebProject.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IGmailAPIService _gmailService;
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;

        private static List<GenericEmailViewModel> _allEmails;


        public EmailController(IEmailService emailService, IApplicationService appService, IGmailAPIService gmailService)
        {
            _emailService = emailService;
            _appService = appService;
            _gmailService = gmailService;
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

        public IActionResult GetNewEmails()
        {
            var vm = new AllEmailsViewModel
            {
                AllEmails = _allEmails.Where(x => x.Status == EmailStatus.New.ToString()).ToList(),
                ActiveTab = "new"
            };

            return View("Index", vm);
        }

        public IActionResult GetOpenEmails()
        {
            var vm = new AllEmailsViewModel
            {
                AllEmails = _allEmails.Where(x => x.Status == EmailStatus.Open.ToString()).ToList(),
                ActiveTab = "open"
            };

            return View("Index", vm);
        }

        public IActionResult GetClosedEmails()
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

        public async Task<IActionResult> MarkInvalid(string id)
        {
            await _emailService.MarkInvalidAsync(id);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var vm = new AllEmailsViewModel
            {
                AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            return View("Index", vm);
        }

        public async Task<IActionResult> MarkNew(string id)
        {
            await _emailService.MarkNewAsync(id);

            var mailId = await _emailService.GetGmailId(id);
            var body = await _gmailService.GetEmailBody(mailId);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var email = emailsIndex.FirstOrDefault(x => x.Id.ToString() == id);
            var vm = new GenericEmailViewModel
            {
                Id = email.Id.ToString(),
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                Status = email.Status.ToString(),
                EmailBody = body
                //AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                //ActiveTab = "all"
            };

            return View("Open", vm);
        }

        public async Task<IActionResult> MarkOpen(string id)
        {
            await _emailService.MarkOpenAsync(id);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var vm = new AllEmailsViewModel
            {
                AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            return View("Index", vm);
        }

        public async Task<IActionResult> MarkNotReviewed(string id)
        {
            await _emailService.MakeNotReviewedAsync(id);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var vm = new AllEmailsViewModel
            {
                AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            return View("Index", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Preview(string id)
        {
            var mailId = await _emailService.GetGmailId(id);
            var body = await _gmailService.GetEmailBody(mailId);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var email = emailsIndex.FirstOrDefault(x => x.Id.ToString() == id);
            var vm = new GenericEmailViewModel
            {
                Id = email.Id.ToString(),
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                Status = email.Status.ToString(),
                EmailBody = body              
            };

            return View(vm);
        }        
    }
}