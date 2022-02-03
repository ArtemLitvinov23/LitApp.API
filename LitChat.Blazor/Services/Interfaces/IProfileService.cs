using System.Threading.Tasks;
using LitChat.Blazor.Models;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserInfo> GetUserInfoAsync(string id);
        Task AddUserInfoAsync(string userId, UserInfo chatMessage);
    }
}
