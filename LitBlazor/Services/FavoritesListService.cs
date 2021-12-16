using LitBlazor.Models;
using LitBlazor.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LitBlazor.Services
{
    public class FavoritesListService : IFavoritesListService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public FavoritesListService(HttpClient httpClient,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task AddUserToFavoriteAsync(FavoritesList favoritesList)
        {

            var request = new HttpRequestMessage(HttpMethod.Post,"api/Favorites");
            var token = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(favoritesList), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task<List<FavoritesListResponse>> GetAllFavoritesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,$"api/Favorites");
            var token = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token.JwtToken);
            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<List<FavoritesListResponse>>();
        }
        public async Task<List<FavoritesListResponse>> GetAllFavoritesForAccountAsync(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Favorites/get-favorites/{id}");
            var token = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.JwtToken);
            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<List<FavoritesListResponse>>();
            return result;
        }

        public async Task<FavoritesListResponse> GetFavoritesUserAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Favorites/{userId}");
            var token = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token.JwtToken);
            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<FavoritesListResponse>();
        }

        public async Task RemoveUserFromFavoriteAsync(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,$"api/Favorites/{id}");
            var token = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token.JwtToken);
            await _httpClient.SendAsync(request);
        }
    }
}
