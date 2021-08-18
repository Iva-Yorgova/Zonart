using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Models.Templates; 

namespace ZonartUsers.Areas.Admin.Controllers
{
    using static WebConstants.Cache;
    public class TemplatesController : AdminController
    {
        private readonly ZonartUsersDbContext data;
        private readonly IMemoryCache cache;

        public TemplatesController(ZonartUsersDbContext data, IMemoryCache cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IActionResult Welcome()
        {
            return View();
        }


        //[ResponseCache(Duration = 3600)]
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
        }       

    }
}
