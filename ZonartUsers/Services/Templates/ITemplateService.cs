

namespace ZonartUsers.Services.Templates
{
    public interface ITemplateService
    {
        bool Edit(
           int templateId,
           string name,
           double price,
           string description,
           string imageUrl);
    }
}
