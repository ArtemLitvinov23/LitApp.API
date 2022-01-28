using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;

namespace LitBlog.BLL.Jwt
{
    public interface IJwtOptions
    {
        public string GenerateJwtToken(AccountDto account);
        public (RefreshToken, Account) GetRefreshToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
        public void RemoveOldRefreshTokens(AccountDto account);
        public string RandomTokenString();
    }
}