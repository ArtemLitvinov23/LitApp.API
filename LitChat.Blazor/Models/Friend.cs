using System;

namespace LitChat.Blazor.Models
{
    public class Friend
    {
        public int RequestById { get; set; }

        public int FriendId { get; set; }

        public string SenderEmail { get; set; }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public string ReceiverEmail { get; set; }

        public string ReceiverFirstName { get; set; }

        public string ReceiverLastName { get; set; }

        public DateTime? RequestTime { get; set; }

        public RequestFlags RequestFlags { get; set; }

        public bool Approved { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? DateOfRejection { get; set; }

        public DateTime? NextRequest { get; set; }
    }
}
