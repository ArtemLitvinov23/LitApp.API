using System;

namespace LitBlog.API.Models
{
    public class AccountResponseViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified { get; set; }
    }
}
