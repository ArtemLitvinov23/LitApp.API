using LitChat.API.Models;
using System;

namespace LitBlog.API.Models
{
    public class ChatMessageModel
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public AccountViewModel FromUser { get; set; }
        public AccountViewModel ToUser { get; set; }
    }
}
