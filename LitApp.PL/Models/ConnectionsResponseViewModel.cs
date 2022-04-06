using System;

namespace LitApp.PL.Models
{
    public class ConnectionsResponseViewModel
    {
        public string UserAccount { get; set; }

        public DateTime ConnectedAt { get; set; }

        public DateTime DisconnectedAt { get; set; }

        public bool IsOnline { get; set; }
    }
}
