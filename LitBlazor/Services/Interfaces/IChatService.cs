using LitBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IChatService
    {
        Task SaveMessageAsync(string userId,ChatMessages chatMessage);
        Task<List<ChatMessages>> GetConversationAsync(string userId,string contactId);
        Task<List<ChatMessages>> GetFullChatHistory(string userId, string contactId);
        Task DeleteChatMessageAsync(string messageId);
        Task DeleteChatHistoryAsync(string userId, string contactId);
    }
}
