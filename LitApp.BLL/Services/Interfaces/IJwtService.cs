using LitApp.BLL.ModelsDto;
using LitApp.DAL.Models;

namespace LitApp.BLL.Services.Interfaces
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