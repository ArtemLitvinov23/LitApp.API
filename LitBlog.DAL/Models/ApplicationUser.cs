using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.DAL.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<ChatMessage> ChatMessagesFromUsers { get; set; }
        public virtual ICollection<ChatMessage> ChatMessagesToUsers { get; set; }
        public ApplicationUser()
        {
            ChatMessagesFromUsers = new HashSet<ChatMessage>();
            ChatMessagesToUsers = new HashSet<ChatMessage>();
        }
    }
}
