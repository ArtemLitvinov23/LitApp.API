using System;

namespace LitBlog.DAL.Models
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        public int FromUserId { get; set; }
        public virtual ApplicationUser FromUser { get; set; }

        public int ToUserId { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
    }
}
