using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.Blazor.Models
{
    public class RegisterAccount
    {
        [Required]
        [StringLength(2-50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(2 - 50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "passwords don't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool AcceptTerms { get; set; }
    }
}
