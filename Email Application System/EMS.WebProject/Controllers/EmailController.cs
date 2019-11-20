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

            foreach (var emailVM in vm.AllEmails)
            {
                emailVM.OperatorUsername = await _appService.GetOperatorUsernameAsync(emailVM.Id);
            }

            return View("Index", vm);
        }

        public async Task<IActionResult> GetClosedEmails()
        {
            var emails = await _emailService.GetClosedEmailsAsync();

            var vm = new AllEmailsViewModel
            {
                AllEmails = emails.Select(mail => mail.MapToViewModel()).ToList(),
                ActiveTab = "closed"
            };

            foreach (var email in vm.AllEmails)
            {
                email.OperatorUsername = await _appService.GetOperatorUsernameAsync(email.Id);
                email.ApplicationStatus = await _appService.GetAppStatus(email.Id);
            }

            return View("Index", vm);
        }

        [HttpGet]
        public async Task<IActionResult> MarkInvalid(string id)
        {
            await _emailService.ChangeStatusAsync(id, EmailStatus.Invalid);

            var allEmails = await _emailService.GetAllEmailsAsync();
            var vm = new AllEmailsViewModel
            {
                AllEmails = allEmails.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = "all"
            };

            return View("Index", vm);
        }


        [HttpGet]
        public async Task<IActionResult> MarkOpen(string id)
        {
            var mailId = await _emailService.GetGmailId(id);
            var body = await _emailService.GetBodyAsync(mailId);

            var email = await _emailService.GetSingleEmailAsync(id);

            var attachmentsVM = new List<AttachmentViewModel>();

            if (email.Attachments.Count != 0)
            {
                foreach (var att in email.Attachments)
                {
                    attachmentsVM.Add(att.MapToViewModel());
                }
            }

            var vm = email.MapToViewModelPreview(body, attachmentsVM);
            vm.InputViewModel.EmailId = id;

            return View("Open", vm);
        }

        [HttpGet]
        public async Task<IActionResult> MarkNew(string id)
        {
            await _emailService.ChangeStatusAsync(id, EmailStatus.New);

            var mailId = await _emailService.GetGmailId(id);
            var body = await _emailService.GetBodyAsync(mailId);

            var email = await _emailService.GetSingleEmailAsync(id);

            var attachmentsVM = new List<AttachmentViewModel>();

            if (email.Attachments.Count != 0)
            {
                foreach (var att in email.Attachments)
                {
                    attachmentsVM.Add(att.MapToViewModel());
                }
            }

            var vm = email.MapToViewModelPreview(body, attachmentsVM);
            vm.InputViewModel.EmailId = id;

            return View("Open", vm);
        }

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

            var email = await _emailService.GetSingleEmailAsync(id);
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