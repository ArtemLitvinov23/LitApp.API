using LitBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<ApplicationUser>> GetUsersAsync();
        Task SaveMessageAsync(ChatMessage chatMessage,string contactId);
        Task<List<ChatMessage>> GetConversationAsync(string contactId, string userId);
        Task<ApplicationUser> GetUserDetailsAsync(string userId);
    }
}
