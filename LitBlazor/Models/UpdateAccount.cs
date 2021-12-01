using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class UpdateAccount
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string UserName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be at least 6 characters")]
        public string LastName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
