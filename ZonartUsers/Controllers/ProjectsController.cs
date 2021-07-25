﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Logos()
        {
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }

        public IActionResult Posters()
        {
            return View();
        }

        public IActionResult Kids()
        {
            return View();
        }

        public IActionResult Web()
        {
            return View();
        }
    }
}
