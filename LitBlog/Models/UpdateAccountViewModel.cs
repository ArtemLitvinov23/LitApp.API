using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class UpdateAccountViewModel
    {
        public string UserName { get; set; }
        public string LastName { get; set; }

    }
}
