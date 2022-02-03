using System;

namespace LitChat.BLL.ModelsDto
{
    public class ConnectionsDto
    {
        public string ConnectionId { get; set; }
        public int UserAccount { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime DisconnectedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}
