using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class ResetPasswordRequestDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}