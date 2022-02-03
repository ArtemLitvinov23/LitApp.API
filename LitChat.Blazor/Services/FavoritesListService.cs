using LitChat.Blazor.Models;
using LitChat.Blazor.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services
{
    public class FavoritesListService : IFavoritesListService
    {
        private readonly IHttpServiceGeneric _httpService;
        public FavoritesListService(IHttpServiceGeneric httpService)
        {
            _httpService = httpService;
        }
        public async Task AddUserToFavoriteAsync(FavoritesList favoritesList) => await _httpService.Post("api/Favorites", favoritesList);
        public async Task<List<FavoritesListResponse>> GetAllFavoritesAsync() => await _httpService.GetAll<FavoritesListResponse>("api/Favorites");
        public async Task<List<FavoritesListResponse>> GetAllFavoritesForAccountAsync(string id) => await _httpService.GetAll<FavoritesListResponse>($"api/Favorites/get-users/{id}");
        public async Task<FavoritesListResponse> GetFavoritesUserAsync(string userId) => await _httpService.Get<FavoritesListResponse>($"api/Favorites/{userId}");
        public async Task RemoveUserFromFavoriteAsync(string id) => await _httpService.Delete($"api/Favorites/{id}");
    }
}
