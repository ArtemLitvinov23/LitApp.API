using LitApp.BLL.ModelsDto;
using LitApp.DAL.Models;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(AccountDto account);
        public Task<(RefreshToken, Account)> GetRefreshToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
        public void RemoveOldRefreshTokens(AccountDto account);
        public string RandomTokenString();
    }
}