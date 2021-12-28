using System;

namespace LitBlazor.Models
{
    public class Connections
    {
        public string ConnectionId { get; set; }
        public string UserAccount { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime DisconnectedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}
