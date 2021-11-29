using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LitBlazor.Models
{
    public class ApplicationUser : IdentityUser
    {
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
