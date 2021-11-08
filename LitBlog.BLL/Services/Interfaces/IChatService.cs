using LitBlog.BLL.ModelsDto;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services
{
   public interface IChatService
    {
        public Task GetConversationAsync(int userId, int contactId);
        public UsersResponseDto GetUser(int userId);
        public UsersResponseDto GetAllUsers();
        public Task SaveMessage(int id,ChatMessageDto chatMessage);
    }
}
