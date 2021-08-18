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
using ZonartUsers.Services.Templates;
using Microsoft.AspNetCore.Authorization;

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
            using var cache = MemoryCacheMock.GetMemoryCache(new List<TemplateListingViewModel>());
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Price = 100, Description = "some" });
            data.Templates.Add(new Template { Name = "test", Price = 50, Description = "some" });
            data.SaveChanges();
        
            var controller = new TemplatesController(data, cache, service);
        
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
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.SaveChanges();

            var controller = new TemplatesController(data, cache, service);

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var viewModel = Assert.IsType<TemplateLayoutModel>(model);

            Assert.Equal(1, data.Templates.Count());

        }


        [Theory]
        [InlineData(1)]
        public void EditShouldReturnViewWithCorrectModel(int id)
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.SaveChanges();

            var controller = new TemplatesController(data, cache, service);

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<TemplateListingViewModel>(model);
        }


        [Theory]
        [InlineData(1, "Name", "Description")]
        public void PostEditShouldReturnRedirectToActionAndEditTemplate(int id, string name, string description)
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Description = "Some" });
            data.SaveChanges();
            
            var controller = new TemplatesController(data, cache, service);

            // Act
            var result = controller.Edit(new TemplateListingViewModel {Id = id, Name = name, Description = description});

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
