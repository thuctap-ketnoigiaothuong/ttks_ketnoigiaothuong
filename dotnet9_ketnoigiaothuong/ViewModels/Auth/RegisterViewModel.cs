﻿using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

}
