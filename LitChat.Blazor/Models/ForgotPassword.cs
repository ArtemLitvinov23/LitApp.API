using System.ComponentModel.DataAnnotations;

namespace LitChat.Blazor.Models
{
    public class ForgotPassword
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
