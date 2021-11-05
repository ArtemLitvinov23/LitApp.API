using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class RegisterAccount
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(typeof(bool), "true","true", ErrorMessage = "Accept Ts & Cs is required")]
        public bool AcceptTerms { get; set; }
    }
}
