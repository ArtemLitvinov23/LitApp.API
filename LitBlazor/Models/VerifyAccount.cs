using System.ComponentModel.DataAnnotations;

namespace LitBlazor.Models
{
    public class VerifyAccount
    {
        [Required(ErrorMessage ="Token is required!")]
        public string Token { get; set; }
    }
}
