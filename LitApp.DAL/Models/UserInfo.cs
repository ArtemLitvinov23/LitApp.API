using System.ComponentModel.DataAnnotations;

namespace LitApp.DAL.Models
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }

        public int AccountId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public Account Account { get; set; }
    }
}
