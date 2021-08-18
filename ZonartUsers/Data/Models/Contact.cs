using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Data.Models
{
    using static GlobalConstants;
    public class Contact
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; }

        public string ImageUrl { get; set; }

    }
}
