using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly BlogContext _blogContext;

        public FavoritesRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task AddUserToFavorites(FavoritesList email)
        {
            await _blogContext.Favorites.AddAsync(email);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteUserFromFavorites(FavoritesList email)
        {
            _blogContext.Favorites.Remove(email);
            await _blogContext.SaveChangesAsync();
        } 
      
        public async Task<FavoritesList> FindUserByEmail(FavoritesList email)
        {
           var response = await _blogContext.Favorites.FindAsync(email);
            return response;
        }


        public IQueryable<FavoritesList> GetAllFavorites() => _blogContext.Favorites.AsQueryable();
    }
}
