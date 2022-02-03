using System.ComponentModel.DataAnnotations;

namespace LitChat.Blazor.Models
{
    public class UpdateAccount
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        public string LastName { get; set; }

        [Phone]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }

    }
}
