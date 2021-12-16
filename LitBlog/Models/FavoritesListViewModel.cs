using System.ComponentModel.DataAnnotations;

namespace LitChat.API.Models
{
    public class FavoritesListViewModel
    {
        public int OwnerAccountId { get; set; }
        public int FavoriteUserAccountId { get; set; }
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
