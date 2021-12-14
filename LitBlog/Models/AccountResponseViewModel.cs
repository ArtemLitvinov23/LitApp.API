using LitChat.API.Models;
using System;

namespace LitBlog.API.Models
{
    public class AccountResponseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleViewModel Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified { get; set; }
    }
}
