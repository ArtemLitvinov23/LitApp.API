using System.Collections.Generic;
using System.Threading.Tasks;
using LitChat.Blazor.Models;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface IAccountService
    {
        Account Account { get; }
        Task Initialize();
        Task Login(AuthAccount model);
        Task Logout();
        Task<Account> GetUserDetailsAsync(string userId);
        Task<List<Account>> GetAllUsersAsync(string currentUserId);
        Task Register(RegisterAccount model);
        Task ResetPassword(ResetPassword model);
        Task ForgotPassword(ForgotPassword model);
        Task Verify(VerifyAccount model);
        Task Update(int userId, UpdateAccount model);
        Task Delete(int id);
    }
}
