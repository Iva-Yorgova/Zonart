using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Orders;

namespace Zonart.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public OrdersController(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public IActionResult Create([FromQuery] int templateId)
        {

            return View(new OrderTemplateModel
            {
                TemplateId = templateId
            });
        }


        [HttpPost]
        public IActionResult Create(OrderTemplateModel info)
        {
            var order = new Order
            {
                TemplateId = info.TemplateId,
                ContactName = info.Name,
                ContactEmail = info.Email,
            };

            this.data.Orders.Add(order);
            this.data.SaveChanges();

            return RedirectToAction("All", "Templates");
        }
    }
}
