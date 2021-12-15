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
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public ChatService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<List<ChatMessages>> GetConversationAsync(string userId,string contactId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Chat/{userId}/{contactId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<List<ChatMessages>>();
        }

        public async Task<Users> GetUserDetailsAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Account/get-users/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<Users>();
        }

        public async Task<List<Users>> GetAllUsersAsync()
        { 
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Account/get-users");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<List<Users>>();
        }

        public async Task SaveMessageAsync(string userId,ChatMessages chatMessage)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Chat/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(chatMessage), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }
    }
}
