using System.Collections.Generic;
using LitBlazor.Models;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IAccountService
    {
        Account Account { get; }
        Task Initialize();
        Task Login(AuthAccount model);
        Task Logout();
        Task Register(RegisterAccount model);
        Task ResetPassword(ResetPassword model);
        Task ForgotPassword(ForgotPassword model);
        Task Verify(VerifyAccount model);
        Task Update(int userId, UpdateAccount model);
        Task Delete(int id);
        Task<Account> GetUserDataFromLocalStorage();
    }
}
