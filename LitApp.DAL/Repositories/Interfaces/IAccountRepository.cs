using LitApp.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.DAL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public IQueryable<Account> GetAllAccounts();

        public Task<Account> GetRefreshToken(string token);

        public Task<Account> GetAccountByIdAsync(int accountId);

        public Task CreateAccountAsync(Account account);

        public Task UpdateAccountAsync(Account update);

        public Task DeleteAsync(int accountId);
    }
}