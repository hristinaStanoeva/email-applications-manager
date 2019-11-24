using EMS.Data;
using EMS.WebProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.FindFirst("IsPasswordChanged").Value == "False")
                    return View();
                else
                    return RedirectToAction("Index", "Email");
            }
            else return LocalRedirect(Constants.ChangePassRedirect);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
