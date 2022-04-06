using LitApp.DAL.Models.Enum;
using System;

namespace LitApp.PL.Models
{
    public class AccountResponseViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Verified { get; set; }

        public DateTime? TokenExpires { get; set; }

        public bool IsVerified { get; set; }
    }
}
