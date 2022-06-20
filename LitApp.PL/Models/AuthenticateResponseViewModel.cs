using System.Text.Json.Serialization;

namespace LitApp.PL.Models
{
    public class AuthenticateResponseViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserInfoViewModel Profile { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
