using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Infrastructure
{
    using static WebConstants;

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
            SeedQuestions(data);
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
            new Template { Price = 10.0, Name = "Eternity", ImageUrl = "../Images/Templates/01.jpg", Description = "Flat summer instagram posts collection with photo"},
            new Template { Price = 12.0, Name = "Elegant" , ImageUrl = "../Images/Templates/02.jpg", Description = "Elegant minimalist design instagram stories"},
            new Template { Price = 13.0, Name = "Wisdom" , ImageUrl = "../Images/Templates/03.jpg", Description = "Instagram botanic puzzle feed"},
            new Template { Price = 9.0, Name = "Waves" , ImageUrl = "../Images/Templates/04.jpg", Description = "Flat fitness instagram posts collection"},
            new Template { Price = 14.0, Name = "Happy" , ImageUrl = "../Images/Templates/05.jpg", Description = "Flat school card template"},
            new Template { Price = 11.0, Name = "Wild" , ImageUrl = "../Images/Templates/06.jpg", Description = "Adopt pet instagram post collection"} 
             });

            data.SaveChanges();
        }

        private static void SeedQuestions(ZonartUsersDbContext data)
        {
            if (data.Questions.Any())
            {
                return;
            }

            data.Questions.AddRange(new[]
            {
            new Question { Text = "Can you make me a logo?", Answer = "100% Yes! Everything we do have unlimited revisions until you are 100% happy with the design. We do logo design, business card design, flyer design, brochure design, stationary, banners and much more as we help you build your brand"},
            new Question { Text = "Do you do small projects?", Answer = "Yes! We are happy to work on small projects; anything from minor edits to a Web site to altering a graphic or logo. Smaller projects are often hourly rated. Contact us to discuss your small project."},
            new Question { Text = "Will I have a say in the graphic design process?", Answer = "Definitely, Our goal is to satisfy you and your input is very important to us. You can give us any idea that you like and we will be happy to emulate and still come up with a design that will be consistent and will represent your whole brand."},
            new Question { Text = "Can you help my current site look more professional?", Answer = "Yes. Give us your requirements and we have experienced expertise to help you give a new professional look that really whistles!"},
            new Question { Text = "Do I have to be local to work with you?", Answer = "Nope! We work with clients all over the world. Our whole team works remotely, allowing us to find the absolute best team for our business."},
            new Question { Text = "What is your response turnaround time?", Answer = "We will respond to all the emails quickly. Approximately the first response can be expected within 90 minutes in our working hours. Within 24 hours all the first responses will be executed."},
            new Question { Text = "How much input do I have in the process?", Answer = "A lot! Your input and feedback is crucial to this process. We’ll start with a lot of questions about your needs, your likes, your wants and work with you to develop just the right look and functionality."},
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


