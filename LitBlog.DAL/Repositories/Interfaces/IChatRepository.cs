using LitBlog.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public interface IChatRepository
    {
        public Task GetConversationAsync(int userId, int contactId);
        public Task SaveMessageAsync(ChatMessage message);
    }
}
