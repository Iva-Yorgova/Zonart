using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Controllers;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ZonartUsers.Tests.Controller
{
    public class AboutControllerTest
    {

        [Fact]
        public void OurStoryShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var aboutController = new AboutController();

            //Act
            var result = aboutController.OurStory();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

        }
    }
}
