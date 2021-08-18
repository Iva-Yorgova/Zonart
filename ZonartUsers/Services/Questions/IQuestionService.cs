

namespace ZonartUsers.Services.Questions
{
    public interface IQuestionService
    {
        bool Edit(
            int questionId,
            string text,
            string answer);

        void Add(
            string text,
            string answer);

        bool Delete(
            int questionId);
    }
}
