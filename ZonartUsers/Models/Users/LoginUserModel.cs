using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Users
{
    public class LoginUserModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
