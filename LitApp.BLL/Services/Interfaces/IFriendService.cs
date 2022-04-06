using LitApp.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IFriendService
    {
        Task SendRequestToUserAsync(int senderId, int friendId);
        Task<List<FriendDto>> GetAllApprovedFriendsAsync(int accountId);
        Task<List<FriendDto>> GetAllPendingRequestsAsync(int accountId);
        Task<List<FriendDto>> GetAllRejectedRequestsAsync(int accountId);
        Task<FriendDto> GetFriendById(int id);
        Task ApprovedUserAsync(int senderId, int friendId);
        Task RejectUserAsync(int senderId, int friendId);
        Task DeleteUserFromFriends(int friendId);
    }
}
