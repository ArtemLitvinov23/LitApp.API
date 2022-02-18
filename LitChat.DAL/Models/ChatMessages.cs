using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitChat.DAL.Models
{
    public class ChatMessages
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public int FromUserId { get; set; }

        public string FromEmail { get; set; }

        [ForeignKey("FromUserId")]
        public Account FromUser { get; set; }

        [ForeignKey("ToUserId")]
        public int ToUserId { get; set; }

        public string ToEmail { get; set; }
        public Account ToUser { get; set; }
    }
}
