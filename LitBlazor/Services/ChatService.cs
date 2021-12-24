using LitBlazor.Models;
using LitBlazor.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services
{
    public class ChatService : IChatService
    {
        private readonly IHttpServiceGeneric _httpService;
        public ChatService(IHttpServiceGeneric httpService)
        {
            _httpService = httpService;
        }
        public async Task<List<ChatMessages>> GetFullChatHistory(string userId, string contactId) => await _httpService.GetAll<ChatMessages>($"api/Chat/full/{userId}/{contactId}");
        public async Task<List<ChatMessages>> GetConversationAsync(string userId,string contactId)=>await _httpService.GetAll<ChatMessages>($"api/Chat/{userId}/{contactId}");//only 4 last messages
        public async Task SaveMessageAsync(string userId,ChatMessages chatMessage) => await _httpService.Post($"api/Chat/{userId}", chatMessage);
    }
}
