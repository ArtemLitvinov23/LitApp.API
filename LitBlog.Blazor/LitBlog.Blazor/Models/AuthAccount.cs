using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.Blazor.Models
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
