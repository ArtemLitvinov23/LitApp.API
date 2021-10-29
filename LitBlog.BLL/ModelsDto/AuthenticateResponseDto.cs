using System;

namespace LitBlog.BLL.ModelsDto
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Verified { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
