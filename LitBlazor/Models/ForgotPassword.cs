using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class ForgotPassword
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
