using System.ComponentModel.DataAnnotations;

namespace LitChat.API.Models
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
