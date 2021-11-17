using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IFavoritesRepository
    {
        IQueryable<FavoritesList> GetAllFavorites();
        Task<FavoritesList> FindUserByEmail(FavoritesList email);
        Task AddUserToFavorites(FavoritesList email);
        Task DeleteUserFromFavorites(FavoritesList email);
    }
}
