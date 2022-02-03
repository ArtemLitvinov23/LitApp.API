using System.ComponentModel.DataAnnotations;

namespace LitChat.API.Models
{
    public class AuthenticateRequestViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
