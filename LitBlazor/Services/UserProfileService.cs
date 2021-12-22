using LitBlazor.Models;
using LitBlazor.Services.Interfaces;
using System.Threading.Tasks;

namespace LitBlazor.Services
{
    public class UserProfileService: IProfileService
    {
        private readonly IHttpServiceGeneric _httpService;
        public UserProfileService(IHttpServiceGeneric httpService)
        {
            _httpService = httpService;
        }
        public async Task<UserInfo> GetUserInfoAsync(string userId) => await _httpService.Get<UserInfo>($"api/Profile/{userId}");
        public async Task AddUserInfoAsync(string userId, UserInfo userInfo) => await _httpService.Patch($"api/Profile/{userId}", userInfo);
        public async Task RemovePhoneFromAccount(string userId)=> await _httpService.Patch($"api/Profile/phone/{userId}");
        public async Task RemoveDescriptionFromAccount(string userId) => await _httpService.Patch($"api/Profile/description/{userId}");
    }
}
