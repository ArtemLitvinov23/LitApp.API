using LitBlog.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services
{
   public interface IChatService
    {
        public Task GetConversationAsync(int userId, int contactId);
        public Task<UsersResponseDto> GetUserAsync(int userId);
        public Task<List<UsersResponseDto>> GetAllUsersAsync();
        public Task SaveMessageAsync(int id,ChatMessageDto chatMessage);
    }
}
