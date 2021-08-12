using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;

using static ZonartUsers.WebConstants;

namespace ZonartUsers.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<ZonartUsersDbContext>();

            data.Database.Migrate();

            SeedTemplates(data);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void SeedTemplates(ZonartUsersDbContext data)
        {
            if (data.Templates.Any())
            {
                return;
            }

            data.Templates.AddRange(new[]
            {
            new Template { Price = 10.0, Name = "Eternity", ImageUrl = "../Images/Templates/01.jpg"},
            new Template { Price = 12.0, Name = "Elegant" , ImageUrl = "../Images/Templates/02.jpg"},
            new Template { Price = 13.0, Name = "Wisdom" , ImageUrl = "../Images/Templates/03.jpg"},
            new Template { Price = 9.0, Name = "Waves" , ImageUrl = "../Images/Templates/04.jpg"},
            new Template { Price = 14.0, Name = "Happy" , ImageUrl = "../Images/Templates/05.jpg"},
            new Template { Price = 11.0, Name = "Wild" , ImageUrl = "../Images/Templates/06.jpg"}
            
        });

            data.SaveChanges();
        }


        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () => {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdminRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@zonart.com";
                const string adminPassword = "admin123";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
            
        }


    }
}


