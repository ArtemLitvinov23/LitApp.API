using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IFavoritesRepository
    {
        IQueryable<List> GetAllFavorites();
        Task<List> FindUserByEmail(List email);
        Task AddUserToFavorites(List email);
        Task DeleteUserFromFavorites(List email);
    }
}
