using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zonart.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Start()
        {
            return View();
        }
    }
}
