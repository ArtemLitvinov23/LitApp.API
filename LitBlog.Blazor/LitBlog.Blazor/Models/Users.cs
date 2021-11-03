using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.Blazor.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email{ get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
    }
}
