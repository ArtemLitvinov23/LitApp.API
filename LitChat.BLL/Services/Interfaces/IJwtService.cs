using LitChat.BLL.ModelsDto;
using LitChat.DAL.Models;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(AccountDto account);
        public (RefreshToken, Account) GetRefreshToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
        public void RemoveOldRefreshTokens(AccountDto account);
        public string RandomTokenString();
    }
}