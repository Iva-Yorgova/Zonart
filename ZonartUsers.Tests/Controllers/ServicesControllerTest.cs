using ZonartUsers.Controllers;
using ZonartUsers.Models.Services;
using ZonartUsers.Services.Statistics;
using ZonartUsers.Tests.Mocks;
using ZonartUsers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ZonartUsers.Tests.Controllers
{    
    public class ServicesControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            // Arrange
            using var data = DatabaseMock.Instance;

            data.Contacts.Add(new Contact { Email = "test", Message = "test", Name = "test" });
            data.Templates.Add(new Template { Name = "test", Price = 100});
            data.Templates.Add(new Template { Name = "test", Price = 50});
            data.Orders.Add(new Order { Name = "test"});
            data.Users.Add(new User { Email = "test", FullName = "test" });
            data.SaveChanges();

            var statsServ = new StatisticsService(data);

            var controller = new ServicesController(statsServ);

            var stats = statsServ.Total();

            // Act
            var result = controller.All();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var viewModel = Assert.IsType<ServicesViewModel>(model);

            Assert.Equal(2, viewModel.TotalTemplates);

            //MyController<ServicesController>
            //    .Instance()
            //    .Calling(c => c.All())
            //    .ShouldReturn()
            //    .View(new ServicesViewModel 
            //    { 
            //        TotalContacts = stats.TotalContacts,
            //        TotalOrders = stats.TotalOrders,
            //        TotalTemplates = stats.TotalTemplates,
            //        TotalUsers = stats.TotalUsers
            //    });
        }
    }
}
