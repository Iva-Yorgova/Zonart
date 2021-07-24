using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Orders;

namespace Zonart.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public OrdersController(ZonartUsersDbContext data, 
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Create([FromQuery] int templateId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            return View(new OrderTemplateModel
            {
                TemplateId = templateId,
                UserId = user.Id
            });
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderTemplateModel info)
        {

            var user = await this.userManager.GetUserAsync(this.User);


            //if (!ModelState.IsValid)
            //{
            //    return View(info);
            //}

            var order = new Order
            {
                TemplateId = info.TemplateId,
                UserId = user.Id,
                Email = user.UserName,
            };

            this.data.Orders.Add(order);
            this.data.SaveChanges();

            return RedirectToAction("All", "Templates");
        }
    }
}
