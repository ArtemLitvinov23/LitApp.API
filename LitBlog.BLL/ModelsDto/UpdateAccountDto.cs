using LitBlog.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class UpdateAccountDto
    {
        public string Title { get; set; }

        [EnumDataType(typeof(Role))]
        public string Role { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8)]
        public string Password { get; set; }


    }
}
