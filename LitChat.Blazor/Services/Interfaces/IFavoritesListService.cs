using LitChat.Blazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface IFavoritesListService
    {
        Task<List<FavoritesListResponse>> GetAllFavoritesAsync();
        Task<List<FavoritesListResponse>> GetAllFavoritesForAccountAsync(string id);
        Task<FavoritesListResponse> GetFavoritesUserAsync(string userId);
        Task AddUserToFavoriteAsync(FavoritesList favoritesList);
        Task RemoveUserFromFavoriteAsync(string id);
    }
}
