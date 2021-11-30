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
        public async Task<List<ChatMessage>> GetConversationAsync(string userId, string contactId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/chat/{userId}/{contactId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.jwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<List<ChatMessage>>();
        }

        public async Task<ApplicationUser> GetUserDetailsAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/chat/users/{userId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.jwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<ApplicationUser>();
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        { 
            var request = new HttpRequestMessage(HttpMethod.Get, "api/chat/users");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.jwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<List<ApplicationUser>>();
        }

        public async Task SaveMessageAsync(ChatMessage chatMessage,string currentUserId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/chat/{currentUserId}");
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.jwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(chatMessage), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }
    }
}
