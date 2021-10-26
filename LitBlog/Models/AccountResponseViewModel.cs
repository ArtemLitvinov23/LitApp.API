using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.API.Models
{
    public class AccountResponseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsVerified { get; set; }
    }
}
