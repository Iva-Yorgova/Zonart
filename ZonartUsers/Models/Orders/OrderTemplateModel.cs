using System.ComponentModel.DataAnnotations;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Models.Orders
{
    using static Data.GlobalConstants;
    public class OrderTemplateModel
    {
        public int TemplateId { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }



        [Required]
        [Display(Name = "Email")]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

    }

}

