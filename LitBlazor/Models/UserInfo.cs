using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class UserInfo
    {
        [Phone]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
