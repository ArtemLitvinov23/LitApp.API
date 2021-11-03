using System.ComponentModel.DataAnnotations;

namespace LitBlog.Blazor.Models
{
    public class ForgotPassword
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
