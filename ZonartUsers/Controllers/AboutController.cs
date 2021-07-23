using Microsoft.AspNetCore.Mvc;

namespace ZonartUsers.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult OurStory()
        {
            return View();
        }
    }
}
