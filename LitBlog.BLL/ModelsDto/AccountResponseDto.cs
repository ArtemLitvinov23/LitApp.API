using System;

namespace LitBlog.BLL.ModelsDto
{
    public class AccountResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsVerified { get; set; }
    }
}