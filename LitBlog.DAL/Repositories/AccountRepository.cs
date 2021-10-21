using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitBlog.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LitBlog.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BlogContext _context;

        public AccountRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Account> GetAllAccounts() => _context.Accounts;

        public Account GetAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public Account GetRefreshToken(string token) =>  _context.Accounts.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));


        public async Task<Account> GetAccountById(int accountId) => await _context.Accounts.FirstOrDefaultAsync();
        

        public async Task CreateAccount(Account account)
        {
            if (account is null)
                throw new NullReferenceException("Account can't be is null");

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            if (account is null)
                throw new NullReferenceException("Account can't be is null");

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public void Delete(int accountId)
        {
            var user = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (user is null)
                throw new NullReferenceException("Account is not found");

            _context.Remove(user);
            _context.SaveChangesAsync();
        }
    }
}
