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
            await _emailService.ChangeStatusAsync(id, EmailStatus.Invalid);

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
            await _emailService.ChangeStatusAsync(id, EmailStatus.New);

            var mailId = await _emailService.GetGmailId(id);
            var body = await _gmailService.GetEmailBody(mailId);

            var emailsIndex = await _emailService.GetAllEmailsAsync();
            var email = emailsIndex.FirstOrDefault(x => x.Id.ToString() == id);

            var attachmentsVM = new List<AttachmentViewModel>();
            var attachmentsDto = await _emailService.GetAttachmentsAsync(id);
            if (attachmentsDto != null)
            {
                foreach (var att in attachmentsDto)
                {
                    attachmentsVM.Add(att.MapToViewModel());
                }
            }

            var vm = email.MapToViewModelPreview(body, attachmentsVM);
            vm.GenericViewModel = email.MapToViewModel();
            vm.InputViewModel.EmailId = id;

            return View("Open", vm);
        }

        //public async Task<IActionResult> MarkOpen(string id)
        //{
        //    await _emailService.MarkOpenAsync(id);

        //    var emailsIndex = await _emailService.GetAllEmailsAsync();
        //    var vm = new AllEmailsViewModel
        //    {
        //        AllEmails = emailsIndex.Select(x => x.MapToViewModel()).ToList(),
        //        ActiveTab = "all"
        //    };

        //    return View("Index", vm);
        //}

        public async Task<IActionResult> MarkNotReviewed(string id)
        {
            await _emailService.ChangeStatusAsync(id, EmailStatus.NotReviewed);

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
            
            var attachmentsVM = new List<AttachmentViewModel>();
            var attachmentsDto = await _emailService.GetAttachmentsAsync(id);
            if (attachmentsDto != null)
            {
                foreach (var att in attachmentsDto)
                {
                    attachmentsVM.Add(att.MapToViewModel());
                }
            }

            var email = await _emailService.GetSingleMail(id);
            var vm = email.MapToViewModelPreview(body,attachmentsVM);
            vm.GenericViewModel = email.MapToViewModel();

            return View(vm);
        }
    }
}