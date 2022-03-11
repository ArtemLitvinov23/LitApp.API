using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitChat.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public UserInfo Profile { get; set; }

        public Role Role { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? Verified { get; set; }

        public bool IsVerified { get; set; }

        public string ResetToken { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? TokenExpires { get; set; }

        public ICollection<ChatMessages> MessagesFromUser { get; set; }

        public ICollection<ChatMessages> MessagesToUser { get; set; }

        public ICollection<Connections> Connections { get; set; }

        public ICollection<FavoritesList> Favorites { get; set; }

        public ICollection<Friend> SentFriendsRequest { get; set; }

        public ICollection<Friend> RecievedFriendRequest { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}