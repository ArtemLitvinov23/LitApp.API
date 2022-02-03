using System.ComponentModel.DataAnnotations;

namespace LitChat.BLL.ModelsDto
{
    public class ResetPasswordRequestDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}