using System;

namespace LitApp.BLL.ModelsDto
{
    public class ConnectionsResponseDto
    {
        public int Id { get; set; }

        public int UserAccount { get; set; }

        public string ConnectionId { get; set; }

        public DateTime ConnectedAt { get; set; }

        public DateTime DisconnectedAt { get; set; }

        public bool IsOnline { get; set; }
    }
}
