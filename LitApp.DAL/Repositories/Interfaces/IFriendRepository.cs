using LitApp.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.DAL.Repositories.Interfaces
{
    public interface IFriendRepository
    {
        IQueryable<Friend> GetAllFriends();

        Task<Friend> GetRequests(FriendRequest friendRequest);

        Task<Friend> GetFriendById(int id);

        Task AddUserToFriends(Friend user);

        Task UpdateFriendsRequestAsync(Friend user);

        Task RemoveUserFromFriends(int id);
    }
}
