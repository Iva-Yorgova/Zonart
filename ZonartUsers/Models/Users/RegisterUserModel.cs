﻿using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Users
{
    public class RegisterUserModel
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }

}
