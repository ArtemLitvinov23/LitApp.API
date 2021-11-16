using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public class FriendsRepository : IFriendsRepository
    {
        private readonly BlogContext _blogContext;

        public FriendsRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }


        public async Task AddUserToFriends(FriendsList email)
        {
            await _blogContext.Friends.AddAsync(email);
            await _blogContext.SaveChangesAsync();
        }



        public async Task DeleteUserFromFriends(FriendsList email)
        {
            _blogContext.Friends.Remove(email);
            await _blogContext.SaveChangesAsync();
        } 
      

        public async Task FindUserByEmail(FriendsList email) => await _blogContext.Friends.FindAsync(email);

        public IQueryable<FriendsList> GetAllFriends() => _blogContext.Friends.AsQueryable();
    }
}
