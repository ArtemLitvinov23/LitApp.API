using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.DAL.Models
{
    public class Account
    {
        [Key]
        public int Id  { get; set; }

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

        public bool IsVerified { get; set; }

        public string ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public virtual ICollection<FavoritesList> Friends { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}