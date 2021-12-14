using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitBlog.DAL.Models
{
    public class ChatMessages
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        public Account FromUser { get; set; }

        [ForeignKey("ToUserId")]
        public int ToUserId { get; set; }
        public Account ToUser { get; set; }
    }
}
