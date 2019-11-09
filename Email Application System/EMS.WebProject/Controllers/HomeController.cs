using EMS.WebProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GmailAPI;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGmailAPIService _gmailService;

        public HomeController(IGmailAPIService gmailService)
        {
            _gmailService = gmailService;
        }
        public async Task<IActionResult> Index()
        {
            //await _gmailService.GmailSync();
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
