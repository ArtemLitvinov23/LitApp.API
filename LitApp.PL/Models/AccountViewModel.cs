using LitApp.BLL.ModelsDto;
using LitApp.DAL.Models.Enum;
using System;
using System.Collections.Generic;

namespace LitApp.PL.Models
{
    public class AccountViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserInfoDto Profile { get; set; }

        public Role Role { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? Verified { get; set; }

        public bool IsVerified { get; set; }

        public string ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
        public DateTime? TokenExpires { get; set; }

        public ICollection<ChatMessageModel> MessagesFromUser { get; set; }
        public ICollection<ChatMessageModel> MessagesToUser { get; set; }

        public ICollection<FriendViewModel> SentFriendsRequest { get; set; }
        public ICollection<FriendViewModel> RecievedFriendRequest { get; set; }
    }
}
