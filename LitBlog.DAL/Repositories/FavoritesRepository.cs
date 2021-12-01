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

        public async Task AddUserToFavorites(List email)
        {
            await _blogContext.Favorites.AddAsync(email);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteUserFromFavorites(List email)
        {
            _blogContext.Favorites.Remove(email);
            await _blogContext.SaveChangesAsync();
        } 
      
        public async Task<List> FindUserByEmail(List email)
        {
           var response = await _blogContext.Favorites.FindAsync(email);
            return response;
        }


        public IQueryable<List> GetAllFavorites() => _blogContext.Favorites.AsQueryable();
    }
}
