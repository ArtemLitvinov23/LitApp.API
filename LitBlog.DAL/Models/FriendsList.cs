using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitBlog.DAL.Models
{
    public class FriendsList
    {
        [Key]
        public int FriendId { get; set; }

        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public string FriendEmail { get; set; }

    }
}
