using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class AuthenticateRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}