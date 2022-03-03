using System;

namespace LitChat.BLL.ModelsDto
{
    public class ChatMessagesDto
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }
    }
}
