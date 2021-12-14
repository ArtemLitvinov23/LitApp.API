using System;

namespace LitBlazor.Models
{
    public class ChatMessage
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public Account FromUser { get; set; }
        public Account ToUser { get; set; }
    }
}
