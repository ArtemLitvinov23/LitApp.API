using LitApp.DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LitApp.DAL.Models
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

        public Role Role { get; set; } = Role.User;

        public string VerificationToken { get; set; }

        public DateTime? Verified { get; set; } 

        public bool IsVerified { get; set; } = false;

        public string ResetToken { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime? Updated { get; set; }

        public DateTime? TokenExpires { get; set; }

        [JsonIgnore]
        public ICollection<ChatMessages> MessagesFromUser { get; set; }

        [JsonIgnore]
        public ICollection<ChatMessages> MessagesToUser { get; set; }

        [JsonIgnore]
        public ICollection<Connections> Connections { get; set; }

        [JsonIgnore]
        public ICollection<FavoritesList> Favorites { get; set; }

        [JsonIgnore]
        public ICollection<Friend> SentFriendsRequest { get; set; }

        [JsonIgnore]
        public ICollection<Friend> RecievedFriendRequest { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}