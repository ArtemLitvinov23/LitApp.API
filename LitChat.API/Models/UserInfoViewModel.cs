using System.ComponentModel.DataAnnotations;

namespace LitChat.API.Models
{
    public class UserInfoViewModel
    {
        [Phone]
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
