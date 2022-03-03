using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AccountResponseDto> GetUserByIdAsync(int id);
        Task<List<AccountResponseDto>> GetAllUsersWithoutCurrentUserAsync(int currentUserId);
    }
}
