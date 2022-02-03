using System;

namespace LitChat.Blazor.Models
{
    public class Connections
    {
        public string UserAccount { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime DisconnectedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}
