
using System.ComponentModel.DataAnnotations;


namespace ZonartUsers.Data.Models
{
    using static GlobalConstants;
    public class Order
    {
        [Required]
        public int Id { get; set; }

        public int TemplateId { get; set; }
        public Template Template { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string ContactName { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string ContactEmail { get; set; }

    }
}
