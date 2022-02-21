using LitChat.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories.Interfaces
{
    public interface IFriendRepository
    {
        IQueryable<Friend> GetAllFriends();
        Task<Friend> GetRequests(Account userAccount, Account friendAccount);
        Task<Friend> GetFriendById(int id);
        Task AddUserToFriends(Friend user);
        Task UpdateFriendsRequestAsync(Friend user);
        Task RemoveUserFromFriends(int id);
    }
}
