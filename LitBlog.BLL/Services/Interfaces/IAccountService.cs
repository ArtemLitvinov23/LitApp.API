using LitBlog.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto authRequest, string ipAddress);
        Task<AuthenticateResponseDto> RefreshTokenAsync(string token, string ipAddress);
        Task RevokeTokenAsync(string token, string ipAddress);
        Task RegisterAsync(AccountDto model, string origin);
        Task VerifyEmailAsync(string token);
        Task ForgotPasswordAsync(ForgotPasswordRequestDto model, string origin);
        Task ValidateResetTokenAsync(RevokeTokenRequestDto model);
        Task ResetPasswordAsync(ResetPasswordRequestDto model);
        Task<List<UsersResponseDto>> GetUsersAsync();
        Task<List<AccountResponseDto>> GetAllAsync();
        Task<AccountResponseDto> GetAccountByIdAsync(int accountId);
        Task<AccountResponseDto> CreateAsync(AccountDto model);
        Task<AccountResponseDto> UpdateAsync(int id, UpdateAccountDto model);
        Task<List<FavoritesResponseDto>> GetAllFavoritesAsync();
        Task<FavoritesResponseDto> GetFavoritesByEmail(FavoritesDto favorites);
        Task AddUserToFavoritesAsync(FavoritesDto favorites);
        Task DeleteUserFromFavoritesAsync(FavoritesDto favorites);
        Task DeleteAsync(int id);
        public bool ExistsAccount(AccountDto model);

    }
}