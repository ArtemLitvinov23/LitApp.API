
using LitBlog.API.Models;
using System;
using System.Collections.Generic;

namespace LitChat.API.Models
{
    public class AccountViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
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
