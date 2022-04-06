using LitApp.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto authRequest, string ipAddress);
        Task<AuthenticateResponseDto> RefreshTokenAsync(string token, string ipAddress);
        Task<StatusEnum> RevokeTokenAsync(string token, string ipAddress);
        Task<StatusEnum> RegisterAsync(AccountDto model, string origin);
        Task<StatusEnum> VerifyEmailAsync(string token);
        Task<StatusEnum> ForgotPasswordAsync(ForgotPasswordRequestDto model, string origin);
        Task<StatusEnum> ValidateResetTokenAsync(RevokeTokenRequestDto model);
        Task<StatusEnum> ResetPasswordAsync(ResetPasswordRequestDto model);
        Task<List<AccountResponseDto>> GetAllAccountsAsync();
        Task<AccountResponseDto> GetAccountByIdAsync(int accountId);
        Task<AccountResponseDto> GetAccountByEmailAsync(string accountEmail);
        Task<AccountResponseDto> UpdateAccountAsync(int id, UpdateAccountDto model);
        Task<StatusEnum> DeleteAccountAsync(int id);
    }
}