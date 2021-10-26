using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class AuthenticateRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
