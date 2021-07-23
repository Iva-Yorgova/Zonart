
using System.ComponentModel.DataAnnotations;


namespace ZonartUsers.Data.Models
{
    using static GlobalConstants;

    public class Client
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        public Bag Bag { get; set; }
    }
}
