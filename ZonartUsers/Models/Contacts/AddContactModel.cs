using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Contacts
{
    using static Data.GlobalConstants;
    public class AddContactModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(MessageMinLength)]
        public string Message { get; set; }
    }
}
