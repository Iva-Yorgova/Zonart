using System;
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Questions;
using ZonartUsers.Models.Users;
using ZonartUsers.Services.Questions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ZonartUsers.Controllers
{
    using static WebConstants.Cache;
    using static WebConstants;
    public class QuestionsController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly IMemoryCache cache;
        private readonly IQuestionService service;

        public QuestionsController(ZonartUsersDbContext data, IMemoryCache cache, IQuestionService service)
        {
            this.data = data;
            this.cache = cache;
            this.service = service;
        }

        //[ResponseCache(Duration = 3600)]
        public IActionResult All()
        {
            var latestQuestions = this.cache.Get<List<QuestionsListingViewModel>>(QuestionsCacheKey);

            if (latestQuestions == null)
            {
                latestQuestions = this.data.Questions
                    .Select(q => new QuestionsListingViewModel
                    {
                        Id = q.Id,
                        Text = q.Text,
                        Answer = q.Answer
                    })
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                this.cache.Set(QuestionsCacheKey, latestQuestions, cacheOptions);
            }

            return View(latestQuestions);
        }


        [Authorize]
        public IActionResult Add()
        {
           //if (!User.IsAdmin())
           //{
           //    return BadRequest("Credentials invalid!");
           //}

            return View(new AddQuestionModel());
        }


        [Authorize]
        [HttpPost]
        public IActionResult Add(AddQuestionModel question)
        {
            if (!User.IsAdmin())
            {
                return BadRequest("Credentials invalid!");
            }

            if (!ModelState.IsValid)
            {
                return View(question);
            }

            this.service.Add(question.Text, question.Answer);

            TempData[GlobalMessageKey] = "Question was added!";

            return RedirectToAction("All", "Questions");
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var question = this.data.Questions
                .Where(q => q.Id == id)
                .Select(q => new EditQuestionModel
                {
                    Id = id,
                    Text = q.Text,
                    Answer = q.Answer
                })
                .FirstOrDefault();

            return View(question);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit(EditQuestionModel question)
        {
            //if (!User.IsAdmin())
            //{
            //    return BadRequest("Credentials invalid!");
            //}

            if (!ModelState.IsValid)
            {
                return View(question);
            }

            var edited = this.service.Edit(
                question.Id,
                question.Text,
                question.Answer);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Question was edited!";

            return RedirectToAction("All", "Questions");
        }


        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            //if (!User.IsAdmin())
            //{
            //    return BadRequest("Credentials invalid!");
            //}

            var deleted = this.service.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Question was deleted!";

            return RedirectToAction("All", "Questions");
        }
    }
}
