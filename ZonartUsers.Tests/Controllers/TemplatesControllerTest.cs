using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Controllers;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using Xunit;
using ZonartUsers.Data;
using ZonartUsers.Models.Contacts;
using ZonartUsers.Data.Models;
using System.Linq;
using ZonartUsers.Tests.Mocks;
using ZonartUsers.Models.Templates;
using System.Collections.Generic;

namespace ZonartUsers.Tests.Controllers
{
    public class TemplatesControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            MyController<TemplatesController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View();
        }


        [Fact]
        public void AllShouldReturnViewWithCorrectModel2()
        {
            // Arrange
            using var data = DatabaseMock.Instance;

            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.Templates.Add(new Template { Name = "test", Price = 50 });
            data.SaveChanges();

            var controller = new TemplatesController(data);

            // Act
            var result = controller.All();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var viewModel = Assert.IsType<List<TemplateListingViewModel>>(model);

            Assert.Equal(2, data.Templates.Count());

        }


        [Theory]
        [InlineData(1)]
        public void DetailsShouldReturnViewWithCorrectModel(int id)
        {
            // Arrange
            using var data = DatabaseMock.Instance;

            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.SaveChanges();

            var controller = new TemplatesController(data);

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var viewModel = Assert.IsType<TemplateLayoutModel>(model);

            Assert.Equal(1, data.Templates.Count());

        }
    }
}
