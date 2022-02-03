using LitChat.Blazor.Models;
using LitChat.Blazor.Services.Interfaces;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services
{
    public class UserProfileService : IProfileService
    {
        private readonly IHttpServiceGeneric _httpService;
        public UserProfileService(IHttpServiceGeneric httpService)
        {
            _httpService = httpService;
        }
        public async Task<UserInfo> GetUserInfoAsync(string userId) => await _httpService.Get<UserInfo>($"api/Profile/{userId}");
        public async Task AddUserInfoAsync(string userId, UserInfo userInfo) => await _httpService.Patch($"api/Profile/{userId}", userInfo);
    }
}
