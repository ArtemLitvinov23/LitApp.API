using LitBlog.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LitBlog.BLL.ModelsDto
{
    public class UpdateAccountDto
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
    }
}
