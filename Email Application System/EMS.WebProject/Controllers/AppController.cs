using EMS.Data;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Roles = "manager, operator")]
    public class AppController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public AppController(IApplicationService appService, IEmailService emailService, ILogger<EmailController> logger)
        {
            _appService = appService;
            _emailService = emailService;
            _logger = logger;
        }      

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> MarkAppNew(string id)
        {
            try
            {
                var emailId = await _appService.GetEmailId(id);

                await _appService.Delete(id);
                _logger.LogInformation(string.Format(Constants.LogEmailDelete, User.Identity.Name, id));

                await _emailService.ChangeStatusAsync(emailId.ToString(), EmailStatus.New);
                _logger.LogInformation(string.Format(Constants.LogEmailNew, User.Identity.Name, id));

                TempData[Constants.TempDataMsg] = Constants.AppNewSucc;

                return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }           
        }

        [HttpGet]
        public async Task<IActionResult> Preview(string id)
        {
            try
            {
                var application = await _appService.GetByMailIdAsync(id);

                var vm = application.MapToViewModelPreview();
                vm.OperatorName = await _appService.GetOperatorUsernameAsync(id);

                return View(vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> Approve(string id)
        {
            try
            {
                await _appService.ChangeStatusAsync(id, ApplicationStatus.Approved);
                _logger.LogInformation(string.Format(Constants.LogAppApproved, User.Identity.Name, id));

                var emailId = await _appService.GetEmailId(id);
                await _emailService.ChangeStatusAsync(emailId, EmailStatus.Closed);
                _logger.LogInformation(string.Format(Constants.LogEmailClosed, User.Identity.Name, emailId));

                TempData[Constants.TempDataMsg] = Constants.AppValidSucc;

                return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> Reject(string id)
        {
            try
            {
                await _appService.ChangeStatusAsync(id, ApplicationStatus.Rejected);
                _logger.LogInformation(string.Format(Constants.LogAppReject, User.Identity.Name, id));

                var emailId = await _appService.GetEmailId(id);
                await _emailService.ChangeStatusAsync(emailId, EmailStatus.Closed);
                _logger.LogInformation(string.Format(Constants.LogEmailClosed, User.Identity.Name, emailId));

                TempData[Constants.TempDataMsg] = Constants.AppInvalidSucc;

                return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication(InputViewModel vm)
        {
            try
            {
                await _appService.CreateAsync(vm.EmailId, User.Identity.Name, vm.EGN, vm.Name, vm.Phone);
                _logger.LogInformation(string.Format(Constants.LogAppCreate, User.Identity.Name, vm.EmailId));

                await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Open);
                _logger.LogInformation(string.Format(Constants.LogEmailOpen, User.Identity.Name, vm.EmailId));

                TempData[Constants.TempDataMsg] = Constants.AppCreateSucc;

                return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }            
        }
        private IActionResult ErrorHandle(Exception ex)
        {
            _logger.LogError(ex.Message);

            TempData[Constants.TempDataMsg] = Constants.ErrorCatch;

            return View(Constants.PageIndex);
        }
    }
}