using LitBlog.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IChatRepository
    {
        public Task<List<ChatMessage>> GetConversationAsync(int userId, int contactId);
        public Task SaveMessageAsync(ChatMessage message);
    }
}
