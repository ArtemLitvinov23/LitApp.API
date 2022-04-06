using LitApp.DAL.Models.Enum;
using System;

namespace LitApp.BLL.ModelsDto
{
    public class AccountResponseDto
    {
        public int Id { get; set; }

        public UserInfoDto Profile { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Verified { get; set; }

        public DateTime? TokenExpires { get; set; }

        public bool IsVerified { get; set; }
    }
}