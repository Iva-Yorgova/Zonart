﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Templates;
using ZonartUsers.Services.Templates;


namespace ZonartUsers.Controllers
{
    using static WebConstants.Cache;
    using static WebConstants;
    public class TemplatesController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly IMemoryCache cache;
        private readonly ITemplateService service;

        public TemplatesController(ZonartUsersDbContext data, IMemoryCache cache, ITemplateService service)
        {
            this.data = data;
            this.cache = cache;
            this.service = service;
        }

        //[ResponseCache(Duration = 3600)]
        public IActionResult All()
        {
            //var latestTemplates = this.cache.Get<List<TemplateListingViewModel>>//(TemplatesCacheKey);
            //
            //if (latestTemplates == null)
            //{
            //    latestTemplates = this.data.Templates
            //    .Select(t => new TemplateListingViewModel
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        ImageUrl = t.ImageUrl,
            //        Price = t.Price
            //    })
            //    .ToList();
            //
            //    var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
            //
            //    this.cache.Set(TemplatesCacheKey, latestTemplates, cacheOptions);
            //}
            //
            //return View(latestTemplates);

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
                    Name = t.Name,
                    Description = t.Description
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
                    Id = id
                })
                .FirstOrDefault();

            return View(template);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(TemplateListingViewModel template)
        {
            if (!User.IsAdmin())
            {
                return BadRequest("Credentials invalid!");
            }

            if (!ModelState.IsValid)
            {
                return View(template);
            }

            var edited = this.service.Edit(
                template.Id,
                template.Name,
                template.Price,
                template.Description,
                template.ImageUrl);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Template was edited!";

            return RedirectToAction("All", "Templates");
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!User.IsAdmin())
            {
                return BadRequest("Credentials invalid!");
            }

            return View(new AddTemplateModel()
            {
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTemplateModel template)
        {
            if (!User.IsAdmin())
            {
                return BadRequest("Credentials invalid!");
            }

            if (!ModelState.IsValid)
            {
                return View(template);
            }

            var newTemplate = new Template
            {
                Name = template.Name,
                Description = template.Description,
                Price = template.Price,
                ImageUrl = template.ImageUrl
            };

            this.data.Templates.Add(newTemplate);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "You template was added!";

            return RedirectToAction("All", "Templates");

        }

    }
}
