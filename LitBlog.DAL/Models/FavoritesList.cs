using LitBlog.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitChat.DAL.Models
{
    public class FavoritesList
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
