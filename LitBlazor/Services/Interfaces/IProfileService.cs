using System.Threading.Tasks;
using LitBlazor.Models;

namespace LitBlazor.Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserInfo> GetUserInfoAsync(string id);
        Task AddUserInfoAsync(string userId, UserInfo chatMessage);
    }
}
