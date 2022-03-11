using System.ComponentModel.DataAnnotations;

namespace LitChat.Blazor.Models
{
    public class Users
    {
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
