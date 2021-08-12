using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Models.Templates;

namespace ZonartUsers.Areas.Admin.Controllers
{
    public class TemplatesController : AdminController
    {
        private readonly ZonartUsersDbContext data;

        public TemplatesController(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult All()
        {
            var templates = this.data.Templates
                .Select(t => new TemplateListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl,
                    Price = t.Price
                })
                .ToList();

            return View(templates);
        }


        public IActionResult Edit(int templateId)
        {
            // Logic here

            return RedirectToAction(nameof(All));
        }

    }
}
