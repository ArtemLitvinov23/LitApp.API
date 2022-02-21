using LitChat.DAL.Models;
using System;
using System.Collections.Generic;

namespace LitChat.BLL.ModelsDto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public RoleDto Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ICollection<ChatMessagesDto> MessagesFromUser { get; set; }
        public ICollection<ChatMessagesDto> MessagesToUser { get; set; }

        public ICollection<FriendDto> SentFriendsRequest { get; set; }
        public ICollection<FriendDto> RecievedFriendRequest { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
