using EMS.Data;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Policy = Constants.AuthPolicy)]
    [Authorize(Roles = "manager, operator")]
    
    public class EmailController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailService emailService, IApplicationService appService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _appService = appService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                var emailsDesc = await _emailService.GetAllEmailsAsync();                

                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                switch (sortOrder)
                {
                    case "Date":
                        emailsDesc = emailsDesc = emailsDesc.OrderBy(mail => mail.Received).ToList();
                        break;
                    case "date_desc":
                        emailsDesc = emailsDesc = emailsDesc.OrderByDescending(mail => mail.Received).ToList();
                        break;
                    default:
                        emailsDesc = emailsDesc = emailsDesc.OrderByDescending(mail => mail.Received).ToList();
                        break;
                }


                var vm = new AllEmailsViewModel
                {
                    AllEmails = emailsDesc.Select(x => x.MapToViewModel()).ToList(),
                    ActiveTab = Constants.TabAll
                };

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNewEmails()
        {
            try
            {
                var allEmails = await _emailService.GetNewEmailsAsync();

                var vm = new AllEmailsViewModel
                {
                    AllEmails = allEmails.Select(mail => mail.MapToViewModel()).ToList(),
                    ActiveTab = Constants.TabNew
                };

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOpenEmails()
        {
            try
            {
                var openEmails = await _emailService.GetOpenEmailsAsync();
                var apps = await _appService.GetOpenAppsAsync();

                var vm = new AllEmailsViewModel
                {
                    AllEmails = openEmails.Select(mail => mail.MapToViewModel()).ToList(),
                    ActiveTab = Constants.TabOpen
                };

                foreach (var emailVM in vm.AllEmails)
                {
                    emailVM.OperatorUsername = await _appService.GetOperatorUsernameAsync(emailVM.Id);
                }

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetClosedEmails()
        {
            try
            {
                var emails = await _emailService.GetClosedEmailsAsync();

                var vm = new AllEmailsViewModel
                {
                    AllEmails = emails.Select(mail => mail.MapToViewModel()).ToList(),
                    ActiveTab = Constants.TabClosed
                };

                foreach (var email in vm.AllEmails)
                {
                    email.OperatorUsername = await _appService.GetOperatorUsernameAsync(email.Id);
                    email.ApplicationStatus = await _appService.GetAppStatus(email.Id);
                }

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }

        }

        public async Task<IActionResult> MarkInvalid(string id)
        {
            try
            {
                await _emailService.ChangeStatusAsync(id, EmailStatus.Invalid);

                _logger.LogInformation(string.Format(Constants.LogEmailInvalid, User.Identity.Name, id));
                TempData[Constants.TempDataMsg] = Constants.EmailInvalidSucc;

                var allEmails = new List<EmailDto>();
                allEmails = await _emailService.GetAllEmailsAsync();
                var vm = new AllEmailsViewModel
                {
                    AllEmails = allEmails.Select(x => x.MapToViewModel()).ToList(),
                    ActiveTab = Constants.TabAll
                };

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MarkNotReviewed(string id)
        {
            try
            {
                await _emailService.ChangeStatusAsync(id, EmailStatus.NotReviewed);

                _logger.LogInformation(string.Format(Constants.LogEmailNotReviewd, User.Identity.Name, id));
                TempData[Constants.TempDataMsg] = Constants.EmailNotReviewedSucc;

                var allEmails = await _emailService.GetAllEmailsAsync();
                var vm = new AllEmailsViewModel
                {
                    AllEmails = allEmails.Select(x => x.MapToViewModel()).ToList(),
                    ActiveTab = Constants.TabAll
                };

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MarkNew(string id)
        {
            try
            {
                await _emailService.ChangeStatusAsync(id, EmailStatus.New);

                _logger.LogInformation(string.Format(Constants.LogEmailNew, User.Identity.Name, id));
                TempData[Constants.TempDataMsg] = Constants.EmailNewSucc;

                var mailId = await _emailService.GetGmailIdAsync(id);
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

                return View(Constants.PageOpen, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MarkOpen(string id)
        {
            try
            {
                var mailId = await _emailService.GetGmailIdAsync(id);
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

                return View(Constants.PageOpen, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }      
                
        [HttpGet]
        public async Task<IActionResult> EmailBody(string id)
        {
            try
            {
                var body = await _emailService.GetBodyAsync(id);
                return Json(body);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }
        private IActionResult ErrorHandle(Exception ex)
        {
            _logger.LogError(ex.Message);

            TempData["globalError"] = Constants.ErrorCatch;

            return View(Constants.PageIndex);
        }
    }
}