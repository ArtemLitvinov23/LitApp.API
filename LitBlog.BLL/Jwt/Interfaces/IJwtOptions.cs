using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;

namespace LitBlog.BLL.Jwt
{
    public interface IJwtOptions
    {
        public string GenerateJwtToken(AccountCreateDto account);

        public (RefreshToken, Account) GetRefreshToken(string token);

        public RefreshToken GenerateRefreshToken(string ipAddress);

        public void RemoveOldRefreshTokens(AccountCreateDto account);

        public string RandomTokenString();
    }
}