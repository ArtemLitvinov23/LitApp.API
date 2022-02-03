using System.ComponentModel.DataAnnotations;

namespace LitChat.Blazor.Models
{
    public class ResetPassword
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "passwords don't match")]
        public string ConfirmPassword { get; set; }

    }
}
