using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;

namespace ZonartUsers.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly ZonartUsersDbContext data;

        public QuestionService(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public void Add(string text, string answer)
        {
            this.data.Questions.Add(new Data.Models.Question { Text = text, Answer = answer });
            this.data.SaveChanges();
        }

        public bool Delete(int questionId)
        {
            var questionData = this.data.Questions
                .FirstOrDefault(t => t.Id == questionId);

            if (questionData == null)
            {
                return false;
            }

            this.data.Questions.Remove(questionData);
            this.data.SaveChanges();

            return true;
        }

        public bool Edit(int questionId, string text, string answer)
        {
            var questionData = this.data.Questions
                .FirstOrDefault(t => t.Id == questionId);

            if (questionData == null)
            {
                return false;
            }

            questionData.Text = text;
            questionData.Answer = answer;

            this.data.SaveChanges();

            return true;
        }
    }
}
