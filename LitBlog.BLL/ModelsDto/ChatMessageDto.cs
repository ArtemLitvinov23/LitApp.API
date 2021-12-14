using System;

namespace LitBlog.BLL.ModelsDto
{
    public class ChatMessagesDto
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public AccountDto FromUser { get; set; }
        public AccountDto ToUser { get; set; }
    }
}
