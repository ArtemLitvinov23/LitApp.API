using System.ComponentModel.DataAnnotations;

namespace LitBlog.API.Models
{
    public class VerifyRequestViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}
