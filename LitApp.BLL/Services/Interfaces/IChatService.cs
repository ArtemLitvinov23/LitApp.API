using LitApp.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IChatService
    {
        public Task<List<ChatMessagesDto>> GetLastFourMessagesAsync(int userId, int contactId);
        public Task<List<ChatMessagesDto>> GetFullHistoryMessagesAsync(int userId, int contactId);
        public Task SaveMessageAsync(int userId, ChatMessagesDto chatMessage);
        Task RemoveMessage(int messageId);
        Task RemoveChatHistory(int userId, int contactId);
    }
}
