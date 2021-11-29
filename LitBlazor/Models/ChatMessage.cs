﻿using System;

namespace LitBlazor.Models
{
    public class ChatMessage
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUser FromUser { get; set; }
        public ApplicationUser ToUser { get; set; }
    }
}