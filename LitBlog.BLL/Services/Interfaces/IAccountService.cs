using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LitBlog.BLL.ModelsDto;

namespace LitBlog.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequest authRequest, string ipAddress);
        Task<AuthenticateResponseDto> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
        Task Register(AccountCreateDto model, string origin);
        Task VerifyEmail(string token);
        Task ForgotPassword(ForgotPasswordRequestDto model, string origin);
        void ValidateResetToken(RevokeTokenRequest model);
        Task ResetPassword(ResetPasswordRequest model);
        IQueryable<AccountResponseDto> GetAll();
        Task<AccountResponseDto> GetAccountById(int accountId);
        Task<AccountResponseDto> Create(AccountCreateDto model);
        Task<AccountResponseDto> Update(int id, UpdateAccount model);
        void Delete(int id);

    }
}