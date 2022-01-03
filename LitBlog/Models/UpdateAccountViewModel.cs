using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class UpdateAccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Description { get; set; }

    }
}
