using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LitBlog.DAL.Models;

namespace LitBlog.DAL.Repositories
{
    public interface IAccountRepository
    {
        public IQueryable<Account> GetAllAccounts();

        public Account GetAccount(int id);

        public Account GetRefreshToken(string token);

        public Task<Account> GetAccountById(int accountId);

        public Task CreateAccount(Account account);

        public Task UpdateAccount(Account update);

        public void Delete(int accountId);
    }
}