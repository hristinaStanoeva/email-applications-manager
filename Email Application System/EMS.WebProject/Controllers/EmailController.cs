using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebProject.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            var emails = await _emailService.GetAllEmailsAsync();            

            var vm = new AllEmailsViewModel
            {
                AllEmails = emails.Select(x => x.MapToViewModel()).ToList()
            };

            return View(vm);
        }
    }
}