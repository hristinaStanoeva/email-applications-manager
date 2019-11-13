using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.WebProject.Models.Applications;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebProject.Controllers
{
    public class AppController : Controller
    {        
        public AppController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MarkOpen(InputViewModel vm)
        {


            return View("Index");
        }
    }
}