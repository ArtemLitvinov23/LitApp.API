using LitApp.DAL;
using LitApp.DAL.Models;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.DAL.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly BlogContext _blogContext;
        public FriendRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task AddUserToFriends(Friend user)
        {
            await _blogContext.Friends.AddAsync(user);
            await _blogContext.SaveChangesAsync();
        }

        public IQueryable<Friend> GetAllFriends() => _blogContext.Friends.Include(x => x.RequestTo.Profile).Include(x => x.RequestBy.Profile).AsQueryable();

        public async Task<Friend> GetFriendById(int id) => await _blogContext.Friends.Include(x => x.RequestTo).FirstOrDefaultAsync(x => x.RequestToId == id || x.RequestById == id);

        public async Task<Friend> GetRequests(Account userAccount, Account friendAccount) => await _blogContext.Friends.FirstOrDefaultAsync(x =>
        x.RequestById == userAccount.Id && x.RequestToId == friendAccount.Id || x.RequestById == friendAccount.Id && x.RequestToId == userAccount.Id);

        public async Task RemoveUserFromFriends(int id)
        {
            var user = _blogContext.Friends.FirstOrDefault(x => x.RequestToId == id || x.RequestById == id);

            if (user != null)
            {
                _blogContext.Friends.Remove(user);
                await _blogContext.SaveChangesAsync();
            }
        }

        public async Task UpdateFriendsRequestAsync(Friend user)
        {
            _blogContext.Friends.Update(user);
            await _blogContext.SaveChangesAsync();
        }
    }
}
