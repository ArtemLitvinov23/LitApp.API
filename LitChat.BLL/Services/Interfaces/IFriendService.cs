using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IFriendService
    {
        Task SendRequestToUserAsync(AccountDto sender, AccountDto friend);
        Task<List<FriendDto>> GetAllApprovedFriendsAsync();
        Task<List<FriendDto>> GetAllPendingRequestsAsync();
        Task<List<FriendDto>> GetAllRejectedRequestsAsync();
        Task<FriendDto> GetFriendById(int id);
        Task ApprovedUserAsync(AccountDto sender, AccountDto friend);
        Task RejectUserAsync(AccountDto account, AccountDto friend);
        Task DeleteUserFromFriends(AccountDto account);
    }
}
