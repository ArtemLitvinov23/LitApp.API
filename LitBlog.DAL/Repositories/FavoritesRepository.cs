using LitBlog.DAL;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly BlogContext _blogContext;
        public FavoritesRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task AddUserToFavorite(FavoritesList user)
        {
            await _blogContext.FavoritesUsers.AddAsync(user);
            await _blogContext.SaveChangesAsync();
        }
        public IQueryable<FavoritesList> GetAllFavoriteUser() => _blogContext.FavoritesUsers.Include(x=>x.Account).AsQueryable();
        public async Task<FavoritesList> GetFavoriteUserById(int id) => await _blogContext.FavoritesUsers.FindAsync(id);
        public async Task RemoveUserFromFavorite(int id)
        {
            var user = await _blogContext.FavoritesUsers.FirstOrDefaultAsync(x => x.Id==id);
            if (user != null)
                _blogContext.Remove(user);
            await _blogContext.SaveChangesAsync();
        }
    }
}
