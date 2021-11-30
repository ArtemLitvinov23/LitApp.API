using LitBlog.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services
{
   public interface IChatService
    {
        public Task GetConversationAsync(string userId, string contactId);
        public Task<UsersResponseDto> GetUserAsync(string userId);
        public Task<List<UsersResponseDto>> GetAllUsersAsync();
        public Task SaveMessageAsync(string id,ChatMessageDto chatMessage);
    }
}
