using System;

namespace LitBlog.API.Models
{
    public class ChatMessageModel
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
    }
}
