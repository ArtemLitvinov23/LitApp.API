using LitBlazor.Models;
using LitBlazor.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LitBlazor.Services
{
    public class UserProfileService: IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public UserProfileService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<UserInfo> GetUserInfoAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Profile/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<UserInfo>();
        }
        public async Task AddUserInfoAsync(string userId, UserInfo userInfo)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/Profile/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(userInfo), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task RemovePhoneFromAccount(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/Profile/phone/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            await _httpClient.SendAsync(request);
        }

        public async Task RemoveDescriptionFromAccount(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/Profile/description/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            await _httpClient.SendAsync(request);
        }
    }
}
