using System.Collections.Generic;

namespace LitBlog.API.Models
{
    public class ApplicationUserViewModel
    {
        public virtual ICollection<ChatMessageModel> ChatMessagesFromUsers { get; set; }
        public virtual ICollection<ChatMessageModel> ChatMessagesToUsers { get; set; }
        public ApplicationUserViewModel()
        {
            ChatMessagesFromUsers = new HashSet<ChatMessageModel>();
            ChatMessagesToUsers = new HashSet<ChatMessageModel>();
        }
    }
}
