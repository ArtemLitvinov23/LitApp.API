using LitBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IFavoritesListService
    {
        Task<List<FavoritesListResponse>> GetAllFavoritesAsync();
        Task<FavoritesListResponse> GetFavoritesUserAsync(string userId);
        Task AddUserToFavoriteAsync(FavoritesList favoritesList);
        Task RemoveUserFromFavoriteAsync(string id);
    }
}
