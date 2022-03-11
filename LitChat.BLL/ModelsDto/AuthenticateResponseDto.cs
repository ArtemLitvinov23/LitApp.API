using LitChat.DAL.Models;
using System;

namespace LitChat.BLL.ModelsDto
{
    public class AuthenticateResponseDto
    {
        public int AccountId { get; set; }

        public string Email { get; set;}

        public UserInfoDto Profile { get; set; }

        public Role Role { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Verified { get; set; }

        public DateTime? TokenExpires { get; set; }

        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
