using System;
using System.Collections.Generic;
using LitBlazor.Models;
using System.Threading.Tasks;

namespace LitBlazor
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
        Task<IList<Account>> GetAll();
        Task<Account> GetById(int id);
        Task Update(UpdateAccount model);
        Task Delete(int id);
        Task<IList<Users>> GetAllUsers();
    }
}
