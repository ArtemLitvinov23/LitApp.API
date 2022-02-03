using System.ComponentModel.DataAnnotations;

namespace LitChat.Blazor.Models
{
    public class FavoritesList
    {
        public string OwnerAccountId { get; set; }
        public string FavoriteUserAccountId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
