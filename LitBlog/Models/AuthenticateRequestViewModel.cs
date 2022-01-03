using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class AuthenticateRequestViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
