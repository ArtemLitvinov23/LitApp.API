using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class FavoritesList
    {
        public string AccountId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
