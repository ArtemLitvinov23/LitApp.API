using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IChatRepository
    {
        public Task GetConversationAsync(string userId, string contactId);
        public Task SaveMessageAsync(ChatMessage message, string userId);
    }
}
