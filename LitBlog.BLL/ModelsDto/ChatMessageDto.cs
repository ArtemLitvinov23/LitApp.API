using System;

namespace LitBlog.BLL.ModelsDto
{
    public class ChatMessageDto
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUserDto FromUser { get; set; }
        public ApplicationUserDto ToUser { get; set; }
    }
}
