using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZonartUsers.Controllers;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Questions;
using ZonartUsers.Models.Users;
using ZonartUsers.Services.Questions;
using ZonartUsers.Tests.Mocks;

namespace ZonartUsers.Tests.Controllers
{
    public class QuestionsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            MyController<QuestionsController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void GetAddShouldReturnViewWithCorrectModel()
        {
            MyController<QuestionsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attribute => attribute.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(new AddQuestionModel());
        }

        [Fact]
        public void AddShouldReturnViewWithCorrectModel()
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new QuestionService(data);

            data.Questions.Add(new Question { Text = "New Question?", Answer = "New Answer." });
            data.SaveChanges();

            var controller = new QuestionsController(data, cache, service);

            // Act
            var result = controller.Add();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<AddQuestionModel>(model);
        }

        [Theory]
        [InlineData(1)]
        public void EditShouldReturnViewWithCorrectModel(int id)
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new QuestionService(data);

            data.Questions.Add(new Question { Text = "Question?", Answer = "Some answer here." });
            data.SaveChanges();

            var controller = new QuestionsController(data, cache, service);

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<EditQuestionModel>(model);
        }


        [Theory]
        [InlineData(1, "New Text", "New Answer")]
        public void PostEditShouldReturnRedirectToActionAndEditTemplate(int id, string text, string answer)
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new QuestionService(data);

            data.Questions.Add(new Question { Text = "Question?", Answer = "Some answer here." });
            data.SaveChanges();

            var controller = new QuestionsController(data, cache, service);

            // Act
            var result = controller.Edit(new EditQuestionModel() { Id = id, Text = text, Answer = answer });
            var question = data.Questions.FirstOrDefault(q => q.Id == id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(text, question.Text);
            Assert.Equal(answer, question.Answer);
        }


    }
}

