using System;

namespace LitApp.PL.Models
{
    public class ConnectionViewModel
    {
        public string ConnectionId { get; set; }
        public int UserAccount { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime DisconnectedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}
