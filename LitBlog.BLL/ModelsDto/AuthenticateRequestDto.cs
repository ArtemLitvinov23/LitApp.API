using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class AuthenticateRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}