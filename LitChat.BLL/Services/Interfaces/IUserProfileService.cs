using LitChat.BLL.ModelsDto;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task AddInfoAboutAccountAsync(int id, UserInfoDto addUserInfoRequest);
        Task RemovePhoneFromAccountAsync(int id);
        Task RemoveDescriptionFromAccountAsync(int id);
        Task<UserInfoDto> GetUserInfoAsync(int id);
    }
}
