using LitChat.DAL;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BlogContext _context;

        public AccountRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Account> GetAllAccounts() => _context.Accounts.AsQueryable();

        public async Task<Account> GetAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) throw new NullReferenceException("Account not found");
            return account;
        }

        public Account GetRefreshToken(string token) => _context.Accounts.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        public async Task<Account> GetAccountByIdAsync(int accountId) => await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);


        public async Task CreateAccountAsync(Account account)
        {
            if (account is null)
                throw new NullReferenceException("Account can't be is null");

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            if (account is null)
                throw new NullReferenceException("Account can't be is null");

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int accountId)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (user is null)
                throw new NullReferenceException("Account is not found");

            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
