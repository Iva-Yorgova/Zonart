
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

        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

    }
}
