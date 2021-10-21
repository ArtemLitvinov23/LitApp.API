using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitBlog.DAL.Models
{
    public class Account
    {
        [Key]
        public int Id  { get; set; }

        public string Title { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Role Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}