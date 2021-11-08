using System.Collections.Generic;

namespace LitBlog.BLL.ModelsDto
{
    public class ApplicationUserDto
    {
        public virtual ICollection<ChatMessageDto> ChatMessagesFromUsers { get; set; }
        public virtual ICollection<ChatMessageDto> ChatMessagesToUsers { get; set; }
        public ApplicationUserDto()
        {
            ChatMessagesFromUsers = new HashSet<ChatMessageDto>();
            ChatMessagesToUsers = new HashSet<ChatMessageDto>();
        }
    }
}
