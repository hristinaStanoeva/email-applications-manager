using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Applications;
using EMS.WebProject.Models.Emails;
using GmailAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Policy = "IsPasswordChanged")]
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

        [HttpGet]
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

        public async Task<IActionResult> GetNewEmails()
        {
            var allEmails = await _emailService.GetNewEmailsAsync();

            var vm = new AllEmailsViewModel
            {
                AllEmails = allEmails.Select(mail => mail.MapToViewModel()).ToList(),
                ActiveTab = "new"
            };

            return View("Index", vm);
        }

        public async Task<IActionResult> GetOpenEmails()
        {
            var allEmails = await _emailService.GetOpenEmailsAsync();
            var apps = await _appService.GetOpenAppsAsync();

            var vm = new AllEmailsViewModel
            {
                AllEmails = allEmails.Select(mail => mail.MapToViewModel()).ToList(),
                ActiveTab = "open"
            };

            var appVM = new List<GenericAppViewModel>();
            foreach (var item in vm.AllEmails)
            {
                foreach (var app in apps)
                {
                    if (app.Email.Id.ToString() == item.Id)
                    {
                        appVM.Add(app.MapToViewModelOpenMail());
                    }
                }
            }

            foreach (var item in vm.AllEmails)
            {
                foreach (var app in appVM)
                {
                    item.AppViewModel = app;
                }
            }

            return View("Index", vm);
        }

        public async Task<IActionResult> GetClosedEmails()
        {
            var apps = await _appService.GetClosedAppsAsync();

            var vm = new AllEmailsViewModel
            {
                AllApps = apps.Select(x => x.MapToViewModel()).ToList(),
                AllEmails = _allEmails.Where(x => x.Status == EmailStatus.Closed.ToString()).ToList(),
                ActiveTab = "closed"
            };

            var mailVM = new List<GenericEmailViewModel>();
            foreach (var item in vm.AllApps)
            {
                foreach (var mail in vm.AllEmails)
                {
                    if (mail.Id == item.Email.Id.ToString())
                    {
                        mailVM.Add(mail);
                    }
                }
            }

            foreach (var item in vm.AllApps)
            {
                foreach (var app in mailVM)
                {
                    item.EmailViewModel = app;
                }
            }


            return View("Index", vm);
        }

        [HttpGet]
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

        //[HttpGet]
        public async Task<IActionResult> MarkNew(string id)
        {
            await _emailService.ChangeStatusAsync(id, EmailStatus.New);

            var mailId = await _emailService.GetGmailId(id);
            var body = await _gmailService.GetEmailBodyAsync(mailId);

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
            //vm.GenericViewModel = email.MapToViewModel();
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
            var body = await _gmailService.GetEmailBodyAsync(mailId);

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
            var previewViewModel = email.MapToViewModelPreview(body, attachmentsVM);
            //previewViewModel.GenericViewModel = email.MapToViewModel();

            return View(previewViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EmailBody(string id)
        {
            var body = await _emailService.GetBodyAsync(id);

            return Json(body);
        }
    }
}