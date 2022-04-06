namespace LitApp.PL.Models
{
    public class ResetPasswordRequestViewModel
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}