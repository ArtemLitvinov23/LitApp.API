
using LitBlog.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitChat.API.Models
{
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public RoleViewModel Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ICollection<ChatMessageModel> MessagesFromUser { get; set; }
        public ICollection<ChatMessageModel> MessagesToUser { get; set; }
    }
}
