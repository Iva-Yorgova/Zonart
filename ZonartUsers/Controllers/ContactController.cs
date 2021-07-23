using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Contacts;

namespace ZonartUsers.Controllers
{
    public class ContactController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public ContactController(ZonartUsersDbContext data)
            => this.data = data;

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddContactFormModel contact)
        {          
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var newContact = new Contact
            {
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message
            };

            this.data.Contacts.Add(newContact);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
