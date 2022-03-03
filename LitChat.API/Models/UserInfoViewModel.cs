using System.ComponentModel.DataAnnotations;

namespace LitChat.API.Models
{
    public class UserInfoViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }
    }
}
