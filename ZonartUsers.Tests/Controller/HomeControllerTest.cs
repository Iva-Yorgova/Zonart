using Microsoft.AspNetCore.Mvc;
using Xunit;
using ZonartUsers.Controllers;
using ZonartUsers.Tests.Mocks;

namespace ZonartUsers.Tests.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var homeController = new HomeController();
            //var data = DatabaseMock.Instance;

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            
        }


        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController();

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void NewTest()
        {
            //Arrange
            var homeController = new HomeController();

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
