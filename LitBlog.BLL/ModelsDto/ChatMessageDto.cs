using System;

namespace LitBlog.BLL.ModelsDto
{
    public class ChatMessageDto
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUserDto FromUser { get; set; }
        public ApplicationUserDto ToUser { get; set; }
    }
}
