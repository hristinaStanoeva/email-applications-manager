using EMS.Data;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Applications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    public class AppController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;


        public AppController(IApplicationService appService, IEmailService emailService)
        {
            _appService = appService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MarkAppNew(string id)
        {
            var emailId = await _appService.GetEmailId(id);

            await _appService.Delete(id);
            await _emailService.ChangeStatusAsync(emailId.ToString(), EmailStatus.New);

            TempData[Constants.TempDataMsg] = Constants.SuccAppNew;

            return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
        }

        [HttpGet]
        public async Task<IActionResult> Preview(string id)
        {
            var application = await _appService.GetByMailIdAsync(id);

            var vm = application.MapToViewModelPreview();
            vm.OperatorName = await _appService.GetOperatorUsernameAsync(id);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(string id)
        {
            await _appService.ChangeStatusAsync(id, ApplicationStatus.Approved);
            var emailId = await _appService.GetEmailId(id);
            await _emailService.ChangeStatusAsync(emailId, EmailStatus.Closed);

            TempData[Constants.TempDataMsg] = Constants.SuccAppInvalid;

            return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
        }

        [HttpGet]
        public async Task<IActionResult> Reject(string id)
        {
            await _appService.ChangeStatusAsync(id, ApplicationStatus.Rejected);
            var emailId = await _appService.GetEmailId(id);
            await _emailService.ChangeStatusAsync(emailId, EmailStatus.Closed);

            TempData[Constants.TempDataMsg] = Constants.SuccAppInvalid;

            return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication(InputViewModel vm)
        {
            await _appService.CreateAsync(vm.EmailId, User.Identity.Name, vm.EGN, vm.Name, vm.Phone);

            await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Open);

            TempData[Constants.TempDataMsg] = Constants.SuccAppCreate;

            return RedirectToAction(Constants.PageIndex, Constants.PageEmail);
        }
    }
}