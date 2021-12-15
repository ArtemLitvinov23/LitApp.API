using LitBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<Users>> GetAllUsersAsync();
        Task SaveMessageAsync(string userId,ChatMessages chatMessage);
        Task<List<ChatMessages>> GetConversationAsync(string userId,string contactId);
        Task<Users> GetUserDetailsAsync(string userId);
    }
}
