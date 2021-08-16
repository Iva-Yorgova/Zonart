
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Contacts;


namespace ZonartUsers.Controllers
{
    public class ContactController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public ContactController(ZonartUsersDbContext data)
        {
            this.data = data;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddContactModel contact)
        {          
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var file = contact.FormFile;
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            var filePath = Path.Combine(basePath, file.FileName);
            
            var contactData = new Contact
            {
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message,
                ImageUrl = filePath
            };

            this.data.Contacts.Add(contactData);
            this.data.SaveChanges();

            return RedirectToAction("Confirm", "Contact");
        }


        public IActionResult Confirm()
        {
            return View();
        }

       
    }
}
