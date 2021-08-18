
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Services.Templates
{
    public class TemplateService : ITemplateService
    {
        private readonly ZonartUsersDbContext data;

        public TemplateService(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public bool Edit(
           int templateId,
           string name,
           double price,
           string description,
           string imageUrl)
        {
            var templateData = this.data.Templates
                .FirstOrDefault(t => t.Id == templateId);

            if (templateData == null)
            {
                return false;
            }

            templateData.Name = name;
            templateData.Price = price;
            templateData.Description = description;
            templateData.ImageUrl = imageUrl;
            
            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int templateId)
        {
            var templateData = this.data.Templates
                .FirstOrDefault(t => t.Id == templateId);

            if (templateData == null)
            {
                return false;
            }

            this.data.Templates.Remove(templateData);
            this.data.SaveChanges();

            return true;
        }

        public void Add(string name, double price, string description, string imageUrl)
        {
            var newTemplate = new Template
            {
                Name = name,
                Description = description,
                Price = price,
                ImageUrl = imageUrl
            };

            this.data.Templates.Add(newTemplate);
            this.data.SaveChanges();
        }
    }
}
