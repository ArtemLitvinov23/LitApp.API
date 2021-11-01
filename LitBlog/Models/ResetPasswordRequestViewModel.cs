﻿using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class ResetPasswordRequestViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}