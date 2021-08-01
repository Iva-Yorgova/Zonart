using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ZonartUsersDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ZonartUsersDbContext data)
        {
            if (data.Templates.Any())
            {
                return;
            }

            data.Templates.AddRange(new[]
            {
            new Template { Price = 100.0, Name = "Eternity", ImageUrl = "../Images/Templates/01.jpg"},
            new Template { Price = 120.0, Name = "Elegant" , ImageUrl = "../Images/Templates/02.jpg"},
            new Template { Price = 130.0, Name = "Wisdom" , ImageUrl = "../Images/Templates/03.jpg"},
            new Template { Price = 100.0, Name = "Waves" , ImageUrl = "../Images/Templates/04.jpg"},
            new Template { Price = 140.0, Name = "Happy" , ImageUrl = "../Images/Templates/05.jpg"},
            new Template { Price = 110.0, Name = "Wild" , ImageUrl = "../Images/Templates/06.jpg"}
            
        });

            data.SaveChanges();
        }
    }
}


