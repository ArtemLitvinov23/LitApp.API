using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class AuthAccount
    {
        [Required]
        [EmailAddress]
        public string Email{ get; set; }

        [Required]
        public string Password { get; set; }
    }
}
