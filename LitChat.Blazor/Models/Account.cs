using System;

namespace LitChat.Blazor.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public string JwtToken { get; set; }

        public DateTime? TokenExpires { get; set; }
    }
}
