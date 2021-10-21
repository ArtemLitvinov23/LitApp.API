using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class UserDto
    {
        [Required] 
        public string UserName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
