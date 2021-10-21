using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class ForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}