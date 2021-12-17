using System.ComponentModel.DataAnnotations;

namespace LitChat.BLL.ModelsDto
{
    public class UserInfoDto
    {
        [Phone]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
