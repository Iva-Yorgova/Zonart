
using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Templates
{
    public class AddTemplateModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

    }
}
