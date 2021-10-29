using LitBlog.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required] 
        [EmailAddress] public string Email { get; set; }

        [Required] 
        [MinLength(8)] public string Password { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public string VerificationToken { get; set; }

        public string ResetToken { get; set; }
    }
}
