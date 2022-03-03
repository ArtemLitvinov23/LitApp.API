using System;

namespace LitChat.DAL.Models
{
    public class Connections
    {
        public int Id { get; set; }

        public string ConnectionId { get; set; } = null!;

        public int UserAccount { get; set; }

        public Account Account { get; set; }

        public DateTime ConnectedAt { get; set; }

        public DateTime DisconnectedAt { get; set; }

        public bool IsOnline { get; set; }
    }
}
