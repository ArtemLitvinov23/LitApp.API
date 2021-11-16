using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IFriendsRepository
    {
        IQueryable<FriendsList> GetAllFriends();
        Task FindUserByEmail(FriendsList email);
        Task AddUserToFriends(FriendsList email);
        Task DeleteUserFromFriends(FriendsList email);
    }
}
