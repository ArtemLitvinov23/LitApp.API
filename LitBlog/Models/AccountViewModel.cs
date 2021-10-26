using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class AccountViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [EnumDataType(typeof(RoleDto))]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
    }
}
