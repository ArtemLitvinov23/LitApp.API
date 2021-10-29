using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class UpdateAccountViewModel
    {

        public string UserName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
