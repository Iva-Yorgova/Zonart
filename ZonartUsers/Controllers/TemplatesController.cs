using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Templates;

namespace ZonartUsers.Controllers
{
    using static WebConstants.Cache;
    public class TemplatesController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly IMemoryCache cache;

        public TemplatesController(ZonartUsersDbContext data, IMemoryCache cache)
        {
            this.data = data;
            this.cache = cache;
        }

        [ResponseCache(Duration = 3600)]
        public IActionResult All()
        {
            var latestTemplates = this.cache.Get<List<TemplateListingViewModel>>(TemplatesCacheKey);

            if (latestTemplates == null)
            {
                latestTemplates = this.data.Templates
                .Select(t => new TemplateListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl,
                    Price = t.Price
                })
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                this.cache.Set(TemplatesCacheKey, latestTemplates, cacheOptions);
            }

            return View(latestTemplates);

            //var templates = this.data.Templates
            //    .Select(t => new TemplateListingViewModel
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //        ImageUrl = t.ImageUrl,
            //        Price = t.Price
            //    })
            //    .ToList();
            //
            //return View(templates);
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

        //[HttpPost]
        //public IActionResult Edit(int templateId, TemplateListingViewModel template)
        //{
        //    var userId = this.User.Id();
        //
        //    if (!User.IsAdmin())
        //    {
        //        return BadRequest("Credentials invalid!");
        //    }
        //
        //
        //
        //    return View(template);
        //}

    }
}
