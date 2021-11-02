using LitBlog.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto authRequest, string ipAddress);
        Task<AuthenticateResponseDto> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
        Task Register(AccountDto model, string origin);
        Task VerifyEmail(string token);
        Task ForgotPassword(ForgotPasswordRequestDto model, string origin);
        void ValidateResetToken(RevokeTokenRequestDto model);
        Task ResetPassword(ResetPasswordRequestDto model);
        IList<UsersResponseDto> GetUsers();
        IList<AccountResponseDto> GetAll();
        Task<AccountResponseDto> GetAccountById(int accountId);
        Task<AccountResponseDto> Create(AccountDto model);
        Task<AccountResponseDto> Update(int id, UpdateAccountDto model);
        void Delete(int id);

    }
}