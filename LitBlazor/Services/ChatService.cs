using LitBlazor.Models;
using LitBlazor.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LitBlazor.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChatMessage>> GetConversationAsync(string contactId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/{contactId}");
        }

        public async Task<ApplicationUser> GetUserDetailsAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/chat/users/{userId}");
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ApplicationUser>>($"api/chat/users");
        }

        public async Task SaveMessageAsync(ChatMessage chatMessage)
        {
            await _httpClient.PostAsJsonAsync("api/chat", chatMessage);
        }
    }
}
