using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UsersResponseDto> GetUserByIdAsync(int id);
        Task<List<UsersResponseDto>> GetAllUsersAsync(int currentUserId);
    }
}
