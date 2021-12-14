using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Templates;
using ZonartUsers.Services.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ZonartUsers.Controllers
{
    using static WebConstants.Cache;
    using static WebConstants;
    public class TemplatesController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly ITemplateService service;

        public TemplatesController(ZonartUsersDbContext data, ITemplateService service)
        {
            this.data = data;
            this.service = service;
        }

        public IActionResult All(string searchTerm, string category, TemplateSorting sorting)
        {
            var templatesQuery = this.data.Templates.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                templatesQuery = templatesQuery
                    .Where(t => t.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                templatesQuery = templatesQuery
                    .Where(t => t.Description.ToLower().Contains(searchTerm.ToLower())
                    || t.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var categories = this.data.Templates
                .Select(t => t.Category)
                .Distinct()
                .ToList();

            templatesQuery = sorting switch
            {
                TemplateSorting.Price => templatesQuery.OrderByDescending(t => t.Price),
                TemplateSorting.Category => templatesQuery.OrderBy(t => t.Category),
                TemplateSorting.DateCreated or _ => templatesQuery.OrderByDescending(t => t.Id)
            };

            var templates = templatesQuery
                .Select(t => new TemplateListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl,
                    Price = t.Price, 
                    Description = t.Description,
                    Category = t.Category
                })
                .ToList();
            
            return View(new AllTemplatesModel 
            { 
                Templates = templates,
                SearchTerm = searchTerm,
                Categories = categories,
                Category = category,
                Sorting = sorting
            });
        }


        public IActionResult Details(int id)
        {
            var template = this.data.Templates
                .Where(t => t.Id == id)
                .Select(t => new TemplateLayoutModel
                {
                    Id = id,
                    Name = t.Name,
                    Description = t.Description,
                    Category = t.Category,
                    Price = t.Price
                })
                .FirstOrDefault();

            return View(template); 
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var template = this.data.Templates
                .Where(t => t.Id == id)
                .Select(t => new TemplateListingViewModel
                {
                    Name = t.Name,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl, 
                    Description = t.Description,
                    Category = t.Category,
                    Id = id
                })
                .FirstOrDefault();

            return View(template);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(TemplateListingViewModel template)
        {
            if (!ModelState.IsValid)
            {
                return View(template);
            }

            var edited = this.service.Edit(
                template.Id,
                template.Name,
                template.Price,
                template.Description,
                template.Category,
                template.ImageUrl);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = TemplateEdited;

            return RedirectToAction("All", "Templates");
        }


        [Authorize]
        public IActionResult Add()
        {
           
            return View(new AddTemplateModel());
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTemplateModel template)
        {
            if (!User.IsAdmin())
            {
                return BadRequest(InvalidCredentials);
            }

            if (!ModelState.IsValid)
            {
                return View(template);
            }

            this.service.Add(template.Name, template.Price, template.Description, template.ImageUrl, template.Category);

            TempData[GlobalMessageKey] = TemplateAdded;

            return RedirectToAction("All", "Templates");

        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int templateId)
        {
            if (!User.IsAdmin())
            {
                return BadRequest(InvalidCredentials);
            }

            var deleted = this.service.Delete(templateId);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = TemplateDeleted;

            return RedirectToAction("All", "Templates");

        }

    }
}
