using LitApp.DAL.Models;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BlogContext _context;
        private readonly IMemoryCache _memoryCache;

        public AccountRepository(
            BlogContext context,
            IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public IQueryable<Account> GetAllAccounts() => _context.Accounts.Include(x => x.Profile).AsQueryable();

        public async Task<Account> GetRefreshToken(string token) => await _context.Accounts.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            Account account = null;

            if (!_memoryCache.TryGetValue(accountId, out account))
            {
                account = await _context.Accounts.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == accountId);
                if (account is not null)
                {
                    _memoryCache.Set(accountId, account, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return account;
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int accountId)
        {
            if (!_memoryCache.TryGetValue(accountId, out var account))
            {
                account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);

                if (account is not null)
                    _context.Remove(account);
            }

            await _context.SaveChangesAsync();
        }
    }
}
