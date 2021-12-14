using LitBlog.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services
{
    public interface IChatService
    {
        public Task<List<ChatMessagesDto>> GetConversationAsync(int userId, int contactId);
        public Task SaveMessageAsync(int userId,ChatMessagesDto chatMessage);
    }
}
