using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Data;


namespace ZonartUsers.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public ProjectsController(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Brochures()
        {
            return View();
        }
    }
}
