using LitBlog.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IChatRepository
    {
        public Task<List<ChatMessages>> GetConversationAsync(int userId, int contactId);
        public Task SaveMessageAsync(ChatMessages message);
    }
}
