using System.ComponentModel.DataAnnotations;

namespace LitChat.Blazor.Models
{
    public class VerifyAccount
    {
        [Required(ErrorMessage = "Token is required!")]
        public string Token { get; set; }
    }
}
