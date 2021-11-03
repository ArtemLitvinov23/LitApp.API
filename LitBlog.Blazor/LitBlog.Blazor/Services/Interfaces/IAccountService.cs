using LitBlog.Blazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.Blazor.Services.Interfaces
{
    public interface IAccountServicexvxcxcv
    {
        Account Account { get; }
        Task Initialize();
        Task Login(AuthAccount model);
        Task Logout();
        Task Register(RegisterAccount model);
        Task ForgotPassword(ForgotPassword model);
        Task Verify(VerifyAccount model);
        Task<IList<Account>> GetAll();
        Task<Account> GetById(int id);
        Task Update(UpdateAccount model);
        Task Delete(int id);
        Task<IList<Users>> GetAllUsers();
    }
}
