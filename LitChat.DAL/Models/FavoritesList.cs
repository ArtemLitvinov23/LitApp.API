using System.ComponentModel.DataAnnotations;

namespace LitChat.DAL.Models
{
    public class FavoritesList
    {
        public int Id { get; set; }

        public int OwnerAccountId { get; set; }

        public int FavoriteUserAccountId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Account FavoriteAccount { get; set; }

        public bool IsDeleted { get; set; }

    }
}
