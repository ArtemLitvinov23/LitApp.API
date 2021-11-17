using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitBlog.DAL.Models
{
    public class FavoritesList
    {
        [Key]
        public int FriendId { get; set; }

        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public string UserEmail { get; set; }

    }
}
