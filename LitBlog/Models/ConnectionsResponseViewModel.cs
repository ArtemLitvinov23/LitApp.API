using System;

namespace LitChat.API.Models
{
    public class ConnectionsResponseViewModel
    {
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public string UserAccount { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime DisconnectedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}
