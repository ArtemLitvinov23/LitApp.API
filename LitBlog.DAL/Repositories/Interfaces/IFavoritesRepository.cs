using LitChat.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories
{
    public interface IFavoritesRepository
    {
        Task<List<FavoritesList>> GetAllFavoriteForAccountUser(int id);

        IQueryable<FavoritesList> GetAllFavoriteUser();
        Task<FavoritesList> GetFavoriteUserById(int id);
        Task AddUserToFavorite(FavoritesList user);
        Task RemoveUserFromFavorite(int id);
        Task RemoveMeFromFavorite(int id);
    }
}
