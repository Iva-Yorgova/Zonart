using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZonartUsers.Data;

using ZonartUsers.Models.Templates;

namespace ZonartUsers.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public TemplatesController(ZonartUsersDbContext data)
        {
            this.data = data;
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


        public IActionResult Details(int id)
        {
            var template = this.data.Templates
                .Where(t => t.Id == id)
                .Select(t => new TemplateLayoutModel
                {
                    Id = id,
                    Name = t.Name
                })
                .FirstOrDefault();

            return View(template); 
        }

    }
}
