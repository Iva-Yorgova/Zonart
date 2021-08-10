using ZonartUsers.Controllers;
using MyTested.AspNetCore.Mvc;
using ZonartUsers.Models.Contacts;
using ZonartUsers.Data.Models;
using System.Linq;
using Xunit;

namespace ZonartUsers.Tests.Controller
{
    public class ContactControllerTest
    {

        // Controller Test
        [Fact]
        public void GetAddShouldReturnViewWithCorrectModel()
        {
            MyController<ContactController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData("Name SomeFamilyName", "email@some.com", "Contact message goes here.")]
        public void PostAddShouldReturnRedirectWithValidModel(string name, 
            string email, string message)
        {
            MyController<ContactController>
                .Instance()
                .Calling(c => c.Add(new AddContactModel
                {
                    Name = name,
                    Email = email,
                    Message = message
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                .WithSet<Contact>(contacts =>
                {
                    contacts.Any(x =>
                    x.Name == name &&
                    x.Email == email &&
                    x.Message == message);
                }))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Confirm", "Contact");
        }


        [Fact]
        public void ConfirmShouldReturnCorrectView()
        {
            MyController<ContactController>
                .Instance()
                .Calling(c => c.Confirm())
                .ShouldReturn()
                .View();
        }


        [Fact]
        public void PipelineTest()
        {
            MyMvc.Pipeline()
                 .ShouldMap("/Contact/Add")
                 .To<ContactController>(c => c.Add())
                 .Which()
                 .ShouldReturn()
                 .View();
        }


        // Route Test
        [Fact]
        public void GetAddShouldBeMapped()
        {
            MyRouting.Configuration()
                 .ShouldMap("/Contact/Add")
                 .To<ContactController>(c => c.Add());
        }

        // Post Test
        [Fact]
        public void PostAddShouldBeMapped()
        {
            MyRouting.Configuration()
                 .ShouldMap(request => request
                 .WithPath("/Contact/Add")
                 .WithMethod(HttpMethod.Post))
                 .To<ContactController>(c => c.Add(With.Any<AddContactModel>()));
        }
    }
}
