using System;
using System.Text.Json.Serialization;

namespace LitChat.API.Models
{
    public class AuthenticateResponseViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserInfoViewModel Profile { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}
